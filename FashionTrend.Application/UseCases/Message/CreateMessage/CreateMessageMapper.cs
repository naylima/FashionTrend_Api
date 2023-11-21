using System;
using AutoMapper;
using FashionTrend.Domain.Entities;

public class CreateMessageMapper : Profile
{
	public CreateMessageMapper()
	{
        CreateMap<CreateMessageRequest, Message>();
        CreateMap<Message, CreateMessageResponse>();
    }
}

