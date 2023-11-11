using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class UpdateProductMapper : Profile
{
	public UpdateProductMapper()
	{
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<Product, UpdateProductResponse>();
    }
}

