using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetOrdersByStatusHandler : IRequestHandler<GetOrdersByStatusRequest, IEnumerable<GetOrdersByStatusResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetOrdersByStatusHandler> _logger;

    public GetOrdersByStatusHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<GetOrdersByStatusHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetOrdersByStatusResponse>>
        Handle(GetOrdersByStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByStatus(request.Status, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetOrdersByStatusResponse>>(order);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving orders by status {RequestStatus}.", request.Status);
            throw;
        }
    }
}