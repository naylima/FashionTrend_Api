using System;
using MediatR;

public sealed record AddMaterialToSupplierRequest(
	Guid MaterialId,
	Guid SupplierId
) : IRequest<AddMaterialToSupplierResponse>;