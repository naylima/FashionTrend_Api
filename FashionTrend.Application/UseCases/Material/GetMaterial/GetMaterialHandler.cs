using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetMaterialHandler : IRequestHandler<GetMaterialRequest, GetMaterialResponse>
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetMaterialHandler> _logger;

    public GetMaterialHandler(IMaterialRepository materialRepository, IMapper mapper, ILogger<GetMaterialHandler> logger)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetMaterialResponse> Handle(GetMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var material = await _materialRepository.Get(request.Id, cancellationToken);

            var response = _mapper.Map<GetMaterialResponse>(material);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving material.");
            throw;
        }
    }
}