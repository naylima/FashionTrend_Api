using System;
using MediatR;

public sealed record AddMaterialToSupplierRequest(
	Guid SupplierId,
    List<Guid> MaterialIds
) : IRequest<IEnumerable<AddMaterialToSupplierResponse>>;