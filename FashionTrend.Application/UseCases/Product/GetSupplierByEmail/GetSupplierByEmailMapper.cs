using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetSupplierByEmailMapper : Profile
{
	public GetSupplierByEmailMapper()
	{
		CreateMap<GetSupplierByEmailRequest, Supplier>();
        CreateMap<Supplier, GetSupplierByEmailResponse>();
    }
}

