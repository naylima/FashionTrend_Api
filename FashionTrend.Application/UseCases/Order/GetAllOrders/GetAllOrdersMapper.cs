using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllOrdersMapper : Profile
{
    public GetAllOrdersMapper()
	{
        CreateMap<GetAllOrdersRequest, Order>();
        CreateMap<Order, GetAllOrdersResponse>();
    }
}