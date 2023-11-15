using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IMaterialRepository _materialRepository;
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(
        IUnitOfWork unitOfWork,
        IMaterialRepository materialRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<CreateProductHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _materialRepository = materialRepository;
		_productRepository = productRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var existingProduct = await _productRepository.GetByName(request.Name, cancellationToken);

            if (existingProduct is not null)
            {
                throw new InvalidOperationException("The provided product name is already registered.");
            }

            var product = _mapper.Map<Product>(request);

            if (request.MaterialIds != null && request.MaterialIds.Any())
            {
                var materials = new List<Material>();

                foreach (var materialId in request.MaterialIds)
                {
                    var material = await _materialRepository.Get(materialId, cancellationToken);

                    if (material is null)
                    {
                        _logger.LogWarning("Material with ID {MaterialId} not found.", materialId);
                    }

                    product.MaterialProducts.Add(new MaterialProduct { Material = material });
                }
            }

            _productRepository.Create(product);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateProductResponse>(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new product with name {ProductName}", request.Name);
            throw;
        }
    }
}
