using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllProductsMapper : Profile
{
	public GetAllProductsMapper()
	{
        CreateMap<GetAllProductsRequest, ProductWithMaterialsDTO>();
        CreateMap<ProductWithMaterialsDTO, GetAllProductsResponse>();
    }
}