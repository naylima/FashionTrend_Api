using System;
using MediatR;

public sealed record GetSupplierByEmailRequest(
    string Email
) : IRequest<GetSupplierByEmailResponse>;