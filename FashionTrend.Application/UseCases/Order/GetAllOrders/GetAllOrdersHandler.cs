using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersRequest, IEnumerable<GetAllOrdersResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllOrdersHandler> _logger;

    public GetAllOrdersHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<GetAllOrdersHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllOrdersResponse>>
        Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetAll(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetAllOrdersResponse>>(order);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all orders.");
            throw;
        }
    }
}