using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper, ILogger<UpdateProductHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.Get(request.Id, cancellationToken);

            if (product is null)
            {
                throw new InvalidOperationException("Product not found. The provided supplier does not exist.");
            }

            _mapper.Map(request, product);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateProductResponse>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the product with ID {ProductId}", request.Id);
            throw;
        }
    }

}

