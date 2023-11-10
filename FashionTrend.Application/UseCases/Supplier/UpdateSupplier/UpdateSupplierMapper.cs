using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class UpdateSupplierMapper : Profile
{
	public UpdateSupplierMapper()
	{
        CreateMap<UpdateSupplierRequest, Supplier>();
        CreateMap<Supplier, UpdateSupplierResponse>();
    }
}

