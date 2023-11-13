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
            var contract = await _contractRepository.Get(request.ContractId, cancellationToken);
            if (contract is null)
            {
                throw new InvalidOperationException("Contract not found. The provided contract does not exist.");
            }

            if(contract.Status != ContractStatus.Active)
            {
                throw new InvalidOperationException("Contract is not active.");
            }

            var supplier = await _supplierRepository.Get(request.SupplierId, cancellationToken);
            if (supplier is null)
            {
                throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
            }

            var requestOrder = await _requestRepository.Get(request.Id, cancellationToken);
            if (requestOrder is null)
            {
                throw new InvalidOperationException("Request not found. The provided request does not exist.");
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

}

