using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateContractMapper : Profile
{
	public CreateContractMapper()
	{
		CreateMap<CreateContractRequest, Contract>();
		CreateMap<Contract, CreateContractResponse>();
	}
}