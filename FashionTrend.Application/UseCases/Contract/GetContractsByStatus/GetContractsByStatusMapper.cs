using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetContractsByStatusMapper : Profile
{
    public GetContractsByStatusMapper()
	{
        CreateMap<GetContractsByStatusRequest, Contract>();
        CreateMap<Contract, GetContractsByStatusResponse>();
    }
}