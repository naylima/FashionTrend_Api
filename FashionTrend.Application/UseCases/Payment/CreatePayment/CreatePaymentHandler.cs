using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestRepository _requestRepository;
    private readonly IPaymentRepository _paymentRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentHandler> _logger;

    public CreatePaymentHandler(
        IUnitOfWork unitOfWork,
        IRequestRepository requestRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper,
        ILogger<CreatePaymentHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _requestRepository = requestRepository;
        _paymentRepository = paymentRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var requestOrder = await _requestRepository.Get(request.RequestId, cancellationToken);

            if (requestOrder == null)
            {
                throw new InvalidOperationException("Request order not found.");
            }

            ValidatePayment(requestOrder, request.Amount);

            var payment = _mapper.Map<Payment>(request);
            _paymentRepository.Create(payment);

            UpdateRemainingAmount(requestOrder, request.Amount);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreatePaymentResponse>(payment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new payment with request {RequestId}", request.RequestId);
            throw;
        }
    }

    private void ValidatePayment(Request requestOrder, decimal paymentAmount)
    {
        if (requestOrder.Status != RequestStatus.Completed)
        {
            throw new InvalidOperationException("Payment can only be made for orders with status 'Completed'.");
        }

        decimal remainingAmount = requestOrder.Value;

        if (paymentAmount > remainingAmount)
        {
            throw new InvalidOperationException("The payment amount exceeds the remaining amount to be paid for the order.");
        }
    }

    private void UpdateRemainingAmount(Request requestOrder, decimal paymentAmount)
    {
        requestOrder.Value -= paymentAmount;

        if (requestOrder.Value == 0)
        {
            requestOrder.Status = RequestStatus.Paid;
        }
    }
}
