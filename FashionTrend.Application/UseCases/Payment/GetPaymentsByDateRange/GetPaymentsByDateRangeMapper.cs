using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetPaymentsByDateRangeMapper : Profile
{
	public GetPaymentsByDateRangeMapper()
	{
        CreateMap<GetPaymentsByDateRangeRequest, Payment>();
        CreateMap<Payment, GetPaymentsByDateRangeResponse>();
    }
}