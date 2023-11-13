using System;
using MediatR;

public sealed record GetPaymentsByDateRangeRequest(
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<IEnumerable<GetPaymentsByDateRangeResponse>>;