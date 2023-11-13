using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetActiveContractsMapper : Profile
{
    public GetActiveContractsMapper()
	{
        CreateMap<GetActiveContractsRequest, Contract>();
        CreateMap<Contract, GetActiveContractsResponse>();
    }
}