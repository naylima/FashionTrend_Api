using System;
using MediatR;

public sealed record CreateSupplierRequest (
	string Email,
	string Name,
	string Password
) : IRequest<CreateSupplierResponse>;