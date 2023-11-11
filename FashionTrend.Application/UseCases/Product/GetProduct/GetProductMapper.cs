using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetProductMapper : Profile
{
	public GetProductMapper()
	{
        CreateMap<GetProductRequest, Product>();
        CreateMap<Product, GetProductResponse>();
    }
}