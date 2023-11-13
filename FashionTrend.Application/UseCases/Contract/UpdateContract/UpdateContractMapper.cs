using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class UpdateContractMapper : Profile
{
	public UpdateContractMapper()
	{
        CreateMap<UpdateContractRequest, Contract>();
        CreateMap<Contract, UpdateContractResponse>();
    }
}

