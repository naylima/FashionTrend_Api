using System;
using MediatR;

namespace FashionTrend.Application.UseCases.Suppliers.CreateSupplier;

public sealed record CreateSupplierRequest (
	string Email,
	string Name,
	string Password
) : IRequest<CreateSupplierResponse>;

