using System;
using System.Threading;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CompleteOrderHandler : IRequestHandler<CompleteOrderRequest, CompleteOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CompleteOrderHandler> _logger;

    public CompleteOrderHandler(
        IUnitOfWork unitOfWork,
        IOrderRepository requestRepository,
        IMapper mapper,
        ILogger<CompleteOrderHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompleteOrderResponse> Handle(CompleteOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var order = await _orderRepository.Get(request.Id, cancellationToken);
            if (order is null)
            {
                throw new InvalidOperationException("Order not found. The provided order does not exist.");
            }

            order.Status = OrderStatus.Completed;

            _mapper.Map(request, order);
            _orderRepository.Update(order);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CompleteOrderResponse>(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the order with ID {RequestId}", request.Id);
            throw;
        }
    }
}

