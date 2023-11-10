using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Application.UseCases.Supplier.Cre;

public class CreateSupplierMapper : Profile
{
	public CreateSupplierMapper()
	{
		CreateMap<CreateSupplierRequest, Supplier>();
		CreateMap<Supplier, CreateSupplierResponse>();
	}
}