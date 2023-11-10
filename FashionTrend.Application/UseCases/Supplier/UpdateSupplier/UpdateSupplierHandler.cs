using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;

public class UpdateSupplierHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public UpdateSupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSupplierResponse> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        var existingSupplier = await _supplierRepository.Get(request.Id, cancellationToken);

        if (existingSupplier == null)
        {
            throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
        }

        _mapper.Map(request, existingSupplier);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UpdateSupplierResponse>(existingSupplier);
    }

}

