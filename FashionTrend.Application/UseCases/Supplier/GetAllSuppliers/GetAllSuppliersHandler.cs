using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllSuppliersHandler : IRequestHandler<GetAllSuppliersRequest, List<GetAllSuppliersResponse>>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllSuppliersHandler> _logger;

    public GetAllSuppliersHandler(ISupplierRepository supplierRepository, IMapper mapper, ILogger<GetAllSuppliersHandler> logger)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetAllSuppliersResponse>> Handle(GetAllSuppliersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var suppliers = await _supplierRepository.GetAll(cancellationToken);

            var response = _mapper.Map<List<GetAllSuppliersResponse>>(suppliers);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all suppliers.");
            throw;
        }
    }
}