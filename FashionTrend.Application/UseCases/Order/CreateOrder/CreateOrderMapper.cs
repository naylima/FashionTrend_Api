using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateOrderMapper : Profile
{
	public CreateOrderMapper()
	{
		CreateMap<CreateOrderRequest, Order>();
		CreateMap<Order, CreateOrderResponse>();
	}
}