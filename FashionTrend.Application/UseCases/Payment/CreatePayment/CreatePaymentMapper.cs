using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreatePaymentMapper : Profile
{
	public CreatePaymentMapper()
	{
		CreateMap<CreatePaymentRequest, Payment>();
		CreateMap<Payment, CreatePaymentResponse>();
	}
}