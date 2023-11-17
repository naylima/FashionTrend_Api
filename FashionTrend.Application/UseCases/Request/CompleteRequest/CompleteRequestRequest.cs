using System;
using MediatR;

public sealed record CompleteRequestRequest (
    Guid Id
    ) : IRequest<CompleteRequestResponse>;