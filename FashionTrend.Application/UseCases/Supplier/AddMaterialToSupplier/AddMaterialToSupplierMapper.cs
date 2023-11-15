using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class AddMaterialToSupplierMapper : Profile
{
	public AddMaterialToSupplierMapper()
	{
		CreateMap<AddMaterialToSupplierRequest, MaterialSupplier>();
		CreateMap<MaterialSupplier, AddMaterialToSupplierResponse>();
	}
}