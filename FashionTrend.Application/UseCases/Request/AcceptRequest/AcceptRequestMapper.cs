using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class AcceptRequestMapper : Profile
{
	public AcceptRequestMapper()
	{
        CreateMap<AcceptRequestRequest, Request>();
        CreateMap<Request, AcceptRequestResponse>();
    }
}

