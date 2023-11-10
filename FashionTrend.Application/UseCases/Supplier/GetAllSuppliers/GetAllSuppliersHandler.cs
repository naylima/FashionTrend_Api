using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;

public class GetAllSuppliersHandler
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;

    public GetAllSuppliersHandler(ISupplierRepository supplierRepository, IMapper mapper)
    {
        _supplierRepository = supplierRepository;
        _mapper = mapper;
    }

    public async Task<GetAllSuppliersResponse> Handle(GetAllSuppliersRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetAll(cancellationToken);

        var response = _mapper.Map<GetAllSuppliersResponse>(supplier);
        return response;
    }
}

