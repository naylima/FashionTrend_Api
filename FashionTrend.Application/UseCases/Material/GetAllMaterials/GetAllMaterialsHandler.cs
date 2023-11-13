using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllMaterialsHandler : IRequestHandler<GetAllMaterialsRequest, IEnumerable<GetAllMaterialsResponse>>
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllMaterialsHandler> _logger;

    public GetAllMaterialsHandler(IMaterialRepository materialRepository, IMapper mapper, ILogger<GetAllMaterialsHandler> logger)
    {
        _materialRepository = materialRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllMaterialsResponse>> Handle(GetAllMaterialsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var materials = await _materialRepository.GetAll(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetAllMaterialsResponse>>(materials);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all materials.");
            throw;
        }
    }
}