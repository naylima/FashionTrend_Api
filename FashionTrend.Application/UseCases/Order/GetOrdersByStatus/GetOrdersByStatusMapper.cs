using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetOrdersByStatusMapper : Profile
{
    public GetOrdersByStatusMapper()
	{
        CreateMap<GetOrdersByStatusRequest, Order>();
        CreateMap<Order, GetOrdersByStatusResponse>();
    }
}