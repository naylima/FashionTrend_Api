using System;
using MediatR;

public sealed record UpdateSupplierRequest (
    Guid Id,
    string Email,
    string Name,
    string Password
) : IRequest<UpdateSupplierResponse>;