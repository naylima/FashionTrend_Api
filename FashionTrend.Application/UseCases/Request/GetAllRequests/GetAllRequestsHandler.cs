using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetAllRequestsHandler : IRequestHandler<GetAllRequestsRequest, IEnumerable<GetAllRequestsResponse>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllRequestsHandler> _logger;

    public GetAllRequestsHandler(IRequestRepository requestRepository, IMapper mapper, ILogger<GetAllRequestsHandler> logger)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetAllRequestsResponse>> Handle(GetAllRequestsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestOrder = await _requestRepository.GetAll(cancellationToken);

            var response = _mapper.Map<IEnumerable<GetAllRequestsResponse>>(requestOrder);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all requests.");
            throw;
        }
    }
}