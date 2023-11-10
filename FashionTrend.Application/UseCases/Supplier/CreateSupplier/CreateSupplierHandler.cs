using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;

public class CreateSupplierHandler
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ISupplierRepository _supplierRepository;
	private readonly IMapper _mapper;

	public CreateSupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_supplierRepository = supplierRepository;
		_mapper = mapper;
	}

	public async Task<CreateSupplierResponse> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
	{
        var existingSupplier = await _supplierRepository.GetByEmail(request.Email, cancellationToken);

        if (existingSupplier != null)
        {
            throw new InvalidOperationException("The provided email is already being used by another supplier.");
        }

        var supplier = _mapper.Map<Supplier>(request);
		_supplierRepository.Create(supplier);

		await _unitOfWork.Commit(cancellationToken);
		return _mapper.Map<CreateSupplierResponse>(supplier);
	}
}

