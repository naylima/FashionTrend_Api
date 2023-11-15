using System;
using MediatR;

public sealed record AcceptRequestRequest (
    Guid Id,
    Guid SupplierId
) : IRequest<AcceptRequestResponse>;