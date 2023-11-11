using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateMaterialMapper : Profile
{
	public CreateMaterialMapper()
	{
		CreateMap<CreateMaterialRequest, Material>();
		CreateMap<Material, CreateMaterialResponse>();
	}
}