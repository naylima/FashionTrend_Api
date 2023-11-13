using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetPaymentsByContractIdMapper : Profile
{
	public GetPaymentsByContractIdMapper()
	{
        CreateMap<GetPaymentsByContractIdRequest, Payment>();
        CreateMap<Payment, GetPaymentsByContractIdResponse>();
    }
}