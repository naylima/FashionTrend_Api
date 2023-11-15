using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateRequestHandler : IRequestHandler<CreateRequestRequest, CreateRequestResponse>
{
	private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IProductRepository _productRepository;
	private readonly IRequestRepository _requestRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateRequestHandler> _logger;

    public CreateRequestHandler(
        IUnitOfWork unitOfWork,
        IContractRepository contractRepository,
        IProductRepository productRepository,
        IRequestRepository requestRepository,
        IMapper mapper,
        ILogger<CreateRequestHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _productRepository = productRepository;
        _requestRepository = requestRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateRequestResponse> Handle(CreateRequestRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var product = await _productRepository.Get(request.ProductId, cancellationToken);

            if(product is null)
            {
                throw new InvalidOperationException("Product not found. The provided product Id does not exist.");
            }
            
            var requestOrder = _mapper.Map<Request>(request);
            requestOrder.SupplierId = null;
            requestOrder.ContractId = null;
            requestOrder.Status = RequestStatus.Pending;

            _requestRepository.Create(requestOrder);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateRequestResponse>(requestOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new request with product {ProductId}", request.ProductId);
            throw;
        }
    }
}
