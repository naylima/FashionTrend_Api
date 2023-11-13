using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IPaymentRepository _paymentRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentHandler> _logger;

    public CreatePaymentHandler(
        IUnitOfWork unitOfWork,
        IContractRepository contractRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper,
        ILogger<CreatePaymentHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _paymentRepository = paymentRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var contract = await _contractRepository.Get(request.ContractId, cancellationToken);

            if (contract == null)
            {
                throw new InvalidOperationException("Contract not found.");
            }

            decimal remainingAmount = contract.TotalValue - contract.Payments.Sum(p => p.Amount);

            if (remainingAmount <= 0)
            {
                throw new InvalidOperationException("The total payment for the contract has already been completed.");
            }

            if (request.Amount > remainingAmount)
            {
                throw new InvalidOperationException("The payment amount exceeds the remaining amount to be paid for the contract.");
            }

            var payment = _mapper.Map<Payment>(request);
            _paymentRepository.Create(payment);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreatePaymentResponse>(payment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new payment with contractId {ContractId}", request.ContractId);
            throw;
        }
    }
}
