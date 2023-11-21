using System;
using System.Threading;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentRepository _paymentRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentHandler> _logger;

    public CreatePaymentHandler(
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper,
        ILogger<CreatePaymentHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _paymentRepository = paymentRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var order = await _orderRepository.Get(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found.");
            }

            decimal remainingAmount = order.Value - (order.Payments.Sum(p => p.Amount) + request.Amount);

            ValidatePayment(order, remainingAmount);

            var payment = _mapper.Map<Payment>(request);
            _paymentRepository.Create(payment);

            await UpdateRemainingAmount(order, request.Amount, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreatePaymentResponse>(payment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new payment with order {OrderId}", request.OrderId);
            throw;
        }
    }

    private void ValidatePayment(Order order, decimal paymentAmount)
    {
        if (order.Status != OrderStatus.Completed)
        {
            throw new InvalidOperationException("Payment can only be made for orders with status 'Completed'.");
        }

        decimal remainingAmount = order.Value - order.Payments.Sum(p => p.Amount);

        if (paymentAmount > remainingAmount)
        {
            throw new InvalidOperationException("The payment amount exceeds the remaining amount to be paid for the order.");
        }
    }

    private async Task UpdateRemainingAmount(Order order, decimal paymentAmount, CancellationToken cancellationToken)
    {
        decimal remainingAmount = order.Value - (order.Payments.Sum(p => p.Amount) + paymentAmount);

        if (order.Value <= 0)
        {
            order.Status = OrderStatus.Paid;
            _orderRepository.Update(order);
            await _unitOfWork.Commit(cancellationToken);
        }
    }
}
