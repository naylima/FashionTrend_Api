using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetOrdersByProductIdHandler : IRequestHandler<GetOrdersByProductIdRequest, IEnumerable<GetOrdersByProductIdResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetOrdersByProductIdHandler> _logger;

    public GetOrdersByProductIdHandler(
        IOrderRepository requestRepository,
        IMapper mapper,
        ILogger<GetOrdersByProductIdHandler> logger)
    {
        _orderRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetOrdersByProductIdResponse>>
        Handle(GetOrdersByProductIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetByProductId(request.ProductId, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetOrdersByProductIdResponse>>(order);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving orders by product {ProductId}.", request.ProductId);
            throw;
        }
    }
}