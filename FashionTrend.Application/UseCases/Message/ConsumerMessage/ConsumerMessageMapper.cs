using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class ConsumerMessageMapper : Profile
{
    public ConsumerMessageMapper()
    {
        CreateMap<ConsumerMessageRequest, MessageReceivedEventArgs>();
    }
}
