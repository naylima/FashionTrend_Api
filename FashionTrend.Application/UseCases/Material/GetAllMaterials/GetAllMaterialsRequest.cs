using System;
using MediatR;

public sealed record GetAllMaterialsRequest () : IRequest<List<GetAllMaterialsResponse>>;