using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class GetRequestsBySupplierIdMapper : Profile
{
    public GetRequestsBySupplierIdMapper()
	{
        CreateMap<GetRequestsBySupplierIdRequest, Request>();
        CreateMap<Request, GetRequestsBySupplierIdResponse>();
    }
}