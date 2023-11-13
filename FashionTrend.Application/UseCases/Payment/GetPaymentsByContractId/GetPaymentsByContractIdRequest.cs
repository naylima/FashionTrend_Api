using System;
using MediatR;

public sealed record GetPaymentsByContractIdRequest(
    Guid ContractId
    ) : IRequest<IEnumerable<GetPaymentsByContractIdResponse>>;