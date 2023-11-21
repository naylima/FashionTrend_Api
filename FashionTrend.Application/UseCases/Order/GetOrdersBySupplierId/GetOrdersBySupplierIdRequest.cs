using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetOrdersBySupplierIdRequest(
        Guid SupplierId
    ) : IRequest<IEnumerable<GetOrdersBySupplierIdResponse>>;