using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateProductMapper : Profile
{
	public CreateProductMapper()
	{
		CreateMap<CreateProductRequest, Product>();
        CreateMap<Product, CreateProductResponse>();
	}
}