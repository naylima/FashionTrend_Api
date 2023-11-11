using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetSupplierByEmailHandler : IRequestHandler<GetSupplierByEmailRequest, GetSupplierByEmailResponse>
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSupplierByEmailHandler> _logger;

    public GetSupplierByEmailHandler(ISupplierRepository supplierRepository, IMapper mapper, ILogger<GetSupplierByEmailHandler> logger)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetSupplierByEmailResponse> Handle(GetSupplierByEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var supplier = await _supplierRepository.GetByEmail(request.Email, cancellationToken);

            if (supplier == null)
            {
                throw new InvalidOperationException("No supplier found with the provided email.");
            }

            var response = _mapper.Map<GetSupplierByEmailResponse>(supplier);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving a supplier by email {SupplierEmail}", request.Email);
            throw;
        }
    }
}

