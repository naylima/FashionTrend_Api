using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetRequestsByStatusMapper : Profile
{
    public GetRequestsByStatusMapper()
	{
        CreateMap<GetRequestsByStatusRequest, Request>();
        CreateMap<Request, GetRequestsByStatusResponse>();
    }
}