using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;

public class GetSupplierByEmailHandler
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public GetSupplierByEmailHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<GetSupplierByEmailResponse> Handle(GetSupplierByEmailRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByEmail(request.Email, cancellationToken);

        if (supplier == null)
        {
            throw new InvalidOperationException("No supplier found with the provided email.");
        }

        var response = _mapper.Map<GetSupplierByEmailResponse>(supplier);
        return response;
    }
}

