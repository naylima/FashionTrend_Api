using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllMaterialsMapper : Profile
{
	public GetAllMaterialsMapper()
	{
        CreateMap<GetAllMaterialsRequest, Material>();
        CreateMap<Material, GetAllMaterialsResponse>();
    }
}