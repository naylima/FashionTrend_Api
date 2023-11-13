using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetActiveContractsHandler : IRequestHandler<GetActiveContractsRequest, IEnumerable<GetActiveContractsResponse>>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetActiveContractsHandler> _logger;

    public GetActiveContractsHandler(IContractRepository contractRepository, IMapper mapper, ILogger<GetActiveContractsHandler> logger)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetActiveContractsResponse>> Handle(GetActiveContractsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contracts = await _contractRepository.GetActiveContracts(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetActiveContractsResponse>>(contracts);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all active contracts.");
            throw;
        }
    }
}