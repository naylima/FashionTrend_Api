using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CompleteOrderMapper : Profile
{
	public CompleteOrderMapper()
	{
        CreateMap<CompleteOrderRequest, Order>();
        CreateMap<Order, CompleteOrderResponse>();
    }
}

