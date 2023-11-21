using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
	private readonly IOrderRepository _orderRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<CreateOrderHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var product = await _productRepository.Get(request.ProductId, cancellationToken);

            if(product is null)
            {
                throw new InvalidOperationException("Product not found. The provided product Id does not exist.");
            }
            
            var order = _mapper.Map<Order>(request);
            order.Status = OrderStatus.Pending;
            order.Value = product.Price * request.Quantity;

            _orderRepository.Create(order);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateOrderResponse>(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new order with product {ProductId}", request.ProductId);
            throw;
        }
    }
}
