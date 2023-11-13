using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<GetAllProductsResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllProductsHandler> _logger;

    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAllProductsHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _productRepository.GetAll(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetAllProductsResponse>>(products);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all products.");
            throw;
        }
    }
}