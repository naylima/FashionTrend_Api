using System;
using System.Threading;
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

            var contract = await ValidateOrCreateContract(supplier.Id, cancellationToken);

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

    private async Task<Contract> ValidateOrCreateContract(Guid supplierId, CancellationToken cancellationToken)
    {
        var existingContract = await _contractRepository.GetActiveContractBySupplierId(supplierId, cancellationToken);

        if (existingContract == null)
        {
            var newContract = new Contract
            {
                ContractNumber = await GenerateContractNumberAsync(cancellationToken),
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddYears(1),
                Status = ContractStatus.Active,
                SupplierId = supplierId,
            };

            _contractRepository.Create(newContract);
            await _unitOfWork.Commit(cancellationToken);

            return newContract;
        }

        return existingContract;
    }

    private async Task<string> GenerateContractNumberAsync(CancellationToken cancellationToken)
    {
        var random = new Random();
        var contractNumber = random.Next(10000, 100000).ToString();

        var existingContract = await _contractRepository.GetByContractNumber(contractNumber, cancellationToken);

        if (existingContract is not null)
        {
            throw new InvalidOperationException("The provided contract number is already registered.");
        }

        return contractNumber;
    }
}

