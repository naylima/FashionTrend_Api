using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetOrdersBySupplierIdMapper : Profile
{
    public GetOrdersBySupplierIdMapper()
	{
        CreateMap<GetOrdersBySupplierIdRequest, Order>();
        CreateMap<Order, GetOrdersBySupplierIdResponse>();
    }
}