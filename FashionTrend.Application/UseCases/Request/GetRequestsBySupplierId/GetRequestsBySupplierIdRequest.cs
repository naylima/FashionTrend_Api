using System;
using FashionTrend.Domain.Enums;
using MediatR;

public sealed record GetRequestsBySupplierIdRequest(
        Guid SupplierId
    ) : IRequest<IEnumerable<GetRequestsBySupplierIdResponse>>;