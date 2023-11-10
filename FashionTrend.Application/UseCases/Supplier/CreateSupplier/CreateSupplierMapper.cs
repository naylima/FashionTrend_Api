using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateSupplierMapper : Profile
{
	public CreateSupplierMapper()
	{
		CreateMap<CreateSupplierRequest, Supplier>();
		CreateMap<Supplier, CreateSupplierResponse>();
	}
}