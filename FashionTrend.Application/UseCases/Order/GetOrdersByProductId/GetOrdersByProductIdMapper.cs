using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetOrdersByProductIdMapper : Profile
{
    public GetOrdersByProductIdMapper()
	{
        CreateMap<GetOrdersByProductIdRequest, Order>();
        CreateMap<Order, GetOrdersByProductIdResponse>();
    }
}