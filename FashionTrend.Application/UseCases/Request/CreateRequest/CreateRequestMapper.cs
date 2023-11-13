using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateRequestMapper : Profile
{
	public CreateRequestMapper()
	{
		CreateMap<CreateRequestRequest, Request>();
		CreateMap<Request, CreateRequestResponse>();
	}
}