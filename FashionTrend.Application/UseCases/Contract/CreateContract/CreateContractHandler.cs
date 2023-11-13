using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateContractHandler : IRequestHandler<CreateContractRequest, CreateContractResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IContractRepository _contractRepository;
    private readonly ISupplierRepository _supplierRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateContractHandler> _logger;

    public CreateContractHandler(
        IUnitOfWork unitOfWork,
        IContractRepository contractRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper,
        ILogger<CreateContractHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _supplierRepository = supplierRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateContractResponse> Handle(CreateContractRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var supplier = await _supplierRepository.Get(request.SupplierId, cancellationToken);
            if (supplier is null)
            {
                throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
            }

            var random = new Random();
            var contractNumber = random.Next(10000, 100000).ToString();

            request = request with { ContractNumber = contractNumber };

            var existingContract = await _contractRepository.GetByContractNumber(request.ContractNumber, cancellationToken);

            if (existingContract is not null)
            {
                throw new InvalidOperationException("The provided contract number is already registered.");
            }

            var contract = _mapper.Map<Contract>(request);
            _contractRepository.Create(contract);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateContractResponse>(contract);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new contract with number {ContractNumber}", request.ContractNumber);
            throw;
        }
    }
}
