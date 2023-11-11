using System;
using MediatR;

public sealed record UpdateMaterialRequest(
    Guid Id,
    string Name,
    string Color
) : IRequest<UpdateMaterialResponse>;