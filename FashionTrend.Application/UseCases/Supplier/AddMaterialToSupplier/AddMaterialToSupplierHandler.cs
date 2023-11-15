using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class AddMaterialToSupplierHandler : IRequestHandler<AddMaterialToSupplierRequest, AddMaterialToSupplierResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IMaterialRepository _materialRepository;
	private readonly ISupplierRepository _supplierRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<AddMaterialToSupplierHandler> _logger;

    public AddMaterialToSupplierHandler(
        IUnitOfWork unitOfWork,
        IMaterialRepository materialRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper,
        ILogger<AddMaterialToSupplierHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _materialRepository = materialRepository;
		_supplierRepository = supplierRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<AddMaterialToSupplierResponse> Handle(AddMaterialToSupplierRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var supplier = await ValidateSupplier(request.SupplierId, cancellationToken);
            var material = await ValidateMaterial(request.MaterialId, cancellationToken);

            if (await SupplierHasMaterial(supplier.Id, material.Id, cancellationToken))
            {
                throw new InvalidOperationException("Supplier already has the specified material.");
            }

            var materialSupplier = _mapper.Map<MaterialSupplier>(request);
            _supplierRepository.AddMaterial(supplier.Id, material.Id, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<AddMaterialToSupplierResponse>(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new material to supplier {SupplierId}", request.SupplierId);
            throw;
        }
    }

    private async Task<Supplier> ValidateSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.Get(supplierId, cancellationToken);
        if (supplier is null)
        {
            throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
        }

        return supplier;
    }

    private async Task<Material> ValidateMaterial(Guid materialId, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.Get(materialId, cancellationToken);
        if (material is null)
        {
            throw new InvalidOperationException("Material not found. The provided material does not exist.");
        }

        return material;
    }

    private async Task<bool> SupplierHasMaterial(Guid supplierId, Guid materialId, CancellationToken cancellationToken)
    {
        return await _supplierRepository.SupplierHasMaterial(supplierId, materialId, cancellationToken);
    }
}
