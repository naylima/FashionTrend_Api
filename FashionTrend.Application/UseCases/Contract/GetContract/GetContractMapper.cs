using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetContractMapper : Profile
{
	public GetContractMapper()
	{
        CreateMap<GetContractRequest, Contract>();
        CreateMap<Contract, GetContractResponse>();
    }
}