using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.Extensions.Logging;

public class CreateSupplierHandler
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ISupplierRepository _supplierRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateSupplierHandler> _logger;

    public CreateSupplierHandler(IUnitOfWork unitOfWork, ISupplierRepository supplierRepository, IMapper mapper, ILogger<CreateSupplierHandler> logger)
	{
		_unitOfWork = unitOfWork;
		_supplierRepository = supplierRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateSupplierResponse> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var existingSupplier = await _supplierRepository.GetByEmail(request.Email, cancellationToken);

            if (existingSupplier is null)
            {
                throw new InvalidOperationException("The provided email is already being used by another supplier.");
            }

            var supplier = _mapper.Map<Supplier>(request);
            _supplierRepository.Create(supplier);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateSupplierResponse>(supplier);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new supplier with email {SupplierEmail}", request.Email);
            throw;
        }
    }
}
