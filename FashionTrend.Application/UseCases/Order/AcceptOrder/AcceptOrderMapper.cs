using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class AcceptOrderMapper : Profile
{
	public AcceptOrderMapper()
	{
        CreateMap<AcceptOrderRequest, Order>();
        CreateMap<Order, AcceptOrderResponse>();
    }
}

