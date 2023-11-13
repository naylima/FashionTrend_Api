using System;
using MediatR;

public sealed record GetAllMaterialsRequest () : IRequest<IEnumerable<GetAllMaterialsResponse>>;