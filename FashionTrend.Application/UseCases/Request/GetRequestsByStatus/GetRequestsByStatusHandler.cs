using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetRequestsByStatusHandler : IRequestHandler<GetRequestsByStatusRequest, IEnumerable<GetRequestsByStatusResponse>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRequestsByStatusHandler> _logger;

    public GetRequestsByStatusHandler(IRequestRepository requestRepository, IMapper mapper, ILogger<GetRequestsByStatusHandler> logger)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetRequestsByStatusResponse>> Handle(GetRequestsByStatusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestOrder = await _requestRepository.GetByStatus(request.Status, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetRequestsByStatusResponse>>(requestOrder);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving requests by status {RequestStatus}.", request.Status);
            throw;
        }
    }
}