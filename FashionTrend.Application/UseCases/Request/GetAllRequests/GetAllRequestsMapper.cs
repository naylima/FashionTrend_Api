using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllRequestsMapper : Profile
{
    public GetAllRequestsMapper()
	{
        CreateMap<GetAllRequestsRequest, Request>();
        CreateMap<Request, GetAllRequestsResponse>();
    }
}