using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.Extensions.Logging;

public class UpdateSupplierHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSupplierHandler> _logger;

    public UpdateSupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IMapper mapper, ILogger<UpdateSupplierHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateSupplierResponse> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var supplier = await _supplierRepository.Get(request.Id, cancellationToken);

            if (supplier is null)
            {
                throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
            }

            _mapper.Map(request, supplier);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateSupplierResponse>(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the supplier with ID {SupplierId}", request.Id);
            throw;
        }
    }

}

