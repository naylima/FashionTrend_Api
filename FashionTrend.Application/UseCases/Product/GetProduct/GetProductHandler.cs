using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetProductHandler : IRequestHandler<GetProductRequest, GetProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetProductHandler> _logger;

    public GetProductHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetProductHandler> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.Get(request.Id, cancellationToken);

            var response = _mapper.Map<GetProductResponse>(product);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving product.");
            throw;
        }
    }
}