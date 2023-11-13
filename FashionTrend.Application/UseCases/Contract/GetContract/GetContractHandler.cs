using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetContractHandler : IRequestHandler<GetContractRequest, GetContractResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetContractHandler> _logger;

    public GetContractHandler(IContractRepository contractRepository, IMapper mapper, ILogger<GetContractHandler> logger)
    {
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetContractResponse> Handle(GetContractRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contract = await _contractRepository.GetByContractNumber(request.ContractNumber, cancellationToken);

            var response = _mapper.Map<GetContractResponse>(contract);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving contract.");
            throw;
        }
    }
}