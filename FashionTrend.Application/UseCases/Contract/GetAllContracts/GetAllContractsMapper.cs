using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetAllContractsMapper : Profile
{
    public GetAllContractsMapper()
	{
        CreateMap<GetAllContractsRequest, Contract>();
        CreateMap<Contract, GetAllContractsResponse>();
    }
}