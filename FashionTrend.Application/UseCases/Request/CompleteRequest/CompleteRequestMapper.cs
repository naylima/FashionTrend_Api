using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CompleteRequestMapper : Profile
{
	public CompleteRequestMapper()
	{
        CreateMap<CompleteRequestRequest, Request>();
        CreateMap<Request, CompleteRequestResponse>();
    }
}

