using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetContractsByStatusHandler : IRequestHandler<GetContractsByStatusRequest, IEnumerable<GetContractsByStatusResponse>>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetContractsByStatusHandler> _logger;

    public GetContractsByStatusHandler(IContractRepository contractRepository, IMapper mapper, ILogger<GetContractsByStatusHandler> logger)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetContractsByStatusResponse>>
        Handle(GetContractsByStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contracts = await _contractRepository.GetContractsByStatus(request.contractStatus, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetContractsByStatusResponse>>(contracts);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all active contracts.");
            throw;
        }
    }
}