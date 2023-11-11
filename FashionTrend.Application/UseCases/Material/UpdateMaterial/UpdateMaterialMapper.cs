using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class UpdateMaterialMapper : Profile
{
	public UpdateMaterialMapper()
	{
        CreateMap<UpdateMaterialRequest, Material>();
        CreateMap<Material, UpdateMaterialResponse>();
    }
}

