using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetMaterialMapper : Profile
{
	public GetMaterialMapper()
	{
        CreateMap<GetMaterialRequest, Material>();
        CreateMap<Material, GetMaterialResponse>();
    }
}