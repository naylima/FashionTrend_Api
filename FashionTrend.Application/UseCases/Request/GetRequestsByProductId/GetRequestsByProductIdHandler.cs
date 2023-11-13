using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetRequestsByProductIdHandler : IRequestHandler<GetRequestsByProductIdRequest, IEnumerable<GetRequestsByProductIdResponse>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRequestsByProductIdHandler> _logger;

    public GetRequestsByProductIdHandler(IRequestRepository requestRepository, IMapper mapper, ILogger<GetRequestsByProductIdHandler> logger)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetRequestsByProductIdResponse>> Handle(GetRequestsByProductIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestOrder = await _requestRepository.GetByProductId(request.ProductId, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetRequestsByProductIdResponse>>(requestOrder);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving requests by product {ProductId}.", request.ProductId);
            throw;
        }
    }
}