using System;
using MediatR;

public sealed record CreateMaterialRequest (
	string Name,
	string Color
) : IRequest<CreateMaterialResponse>;