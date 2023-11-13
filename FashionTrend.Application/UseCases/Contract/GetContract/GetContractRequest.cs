using System;
using MediatR;

public sealed record GetContractRequest(
    string ContractNumber
    ) : IRequest<GetContractResponse>;