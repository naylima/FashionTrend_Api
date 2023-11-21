using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetOrdersBySupplierIdHandler : IRequestHandler<GetOrdersBySupplierIdRequest, IEnumerable<GetOrdersBySupplierIdResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetOrdersBySupplierIdHandler> _logger;

    public GetOrdersBySupplierIdHandler(
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<GetOrdersBySupplierIdHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetOrdersBySupplierIdResponse>>
        Handle(GetOrdersBySupplierIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _orderRepository.GetBySupplierId(request.SupplierId, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetOrdersBySupplierIdResponse>>(order);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving orders by supplier {SupplierId}.", request.SupplierId);
            throw;
        }
    }
}