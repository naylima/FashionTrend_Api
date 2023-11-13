using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetRequestsByProductIdMapper : Profile
{
    public GetRequestsByProductIdMapper()
	{
        CreateMap<GetRequestsByProductIdRequest, Request>();
        CreateMap<Request, GetRequestsByProductIdResponse>();
    }
}