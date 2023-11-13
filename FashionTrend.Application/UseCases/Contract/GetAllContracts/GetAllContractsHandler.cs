using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllContractsHandler : IRequestHandler<GetAllContractsRequest, IEnumerable<GetAllContractsResponse>>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllContractsHandler> _logger;

    public GetAllContractsHandler(IContractRepository contractRepository, IMapper mapper, ILogger<GetAllContractsHandler> logger)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllContractsResponse>> Handle(GetAllContractsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contracts = await _contractRepository.GetAll(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetAllContractsResponse>>(contracts);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all contracts.");
            throw;
        }
    }
}