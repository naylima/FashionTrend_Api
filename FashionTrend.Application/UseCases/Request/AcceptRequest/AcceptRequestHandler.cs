using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class AcceptRequestHandler : IRequestHandler<AcceptRequestRequest, AcceptRequestResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AcceptRequestHandler> _logger;

    public AcceptRequestHandler(
        IUnitOfWork unitOfWork,
        IContractRepository contractRepository,
        IRequestRepository requestRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper,
        ILogger<AcceptRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _requestRepository = requestRepository;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AcceptRequestResponse> Handle(AcceptRequestRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contract = await ValidateContract(request, cancellationToken);
            var supplier = await ValidateSupplier(request.SupplierId, cancellationToken);

            var requestOrder = await _requestRepository.Get(request.Id, cancellationToken);
            if (requestOrder is null)
            {
                throw new InvalidOperationException("Request not found. The provided request does not exist.");
            }

            bool hasMaterials = await SupplierHasMaterials(supplier.Id, requestOrder.ProductId, cancellationToken);
            if (!hasMaterials)
            {
                throw new InvalidOperationException("Supplier does not have the required materials for the product.");
            }

            requestOrder.ContractId = contract.Id;
            requestOrder.SupplierId = supplier.Id;
            requestOrder.Status = RequestStatus.Accepted;

            _mapper.Map(request, requestOrder);
            _requestRepository.Update(requestOrder);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<AcceptRequestResponse>(requestOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the request with ID {RequestId}", request.Id);
            throw;
        }
    }

    private async Task<Contract> ValidateContract(AcceptRequestRequest request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.Get(request.ContractId, cancellationToken);
        if (contract is null)
        {
            throw new InvalidOperationException("Contract not found. The provided contract does not exist.");
        }

        if (contract.SupplierId != request.SupplierId)
        {
            throw new InvalidOperationException("Invalid contract. The contract's supplier does not match the request's supplier.");
        }


        if (contract.Status != ContractStatus.Active)
        {
            throw new InvalidOperationException("Contract is not active.");
        }

        return contract;
    }

    private async Task<Supplier> ValidateSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.Get(supplierId, cancellationToken);
        if (supplier is null)
        {
            throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
        }

        return supplier;
    }

    private async Task<bool> SupplierHasMaterials(Guid supplierId, Guid productId, CancellationToken cancellationToken)
    {
        return await _supplierRepository.SupplierHasMaterials(supplierId, productId, cancellationToken);
    }
}

