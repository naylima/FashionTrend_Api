using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllSuppliersMapper : Profile
{
	public GetAllSuppliersMapper()
	{
        CreateMap<GetAllSuppliersRequest, Supplier>();
        CreateMap<Supplier, GetAllSuppliersResponse>();
    }
}