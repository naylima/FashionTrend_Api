using System;
using MediatR;

public sealed record GetMaterialRequest (
    Guid Id
    ) : IRequest<GetMaterialResponse>;