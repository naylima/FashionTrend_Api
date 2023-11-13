using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetRequestsBySupplierIdHandler : IRequestHandler<GetRequestsBySupplierIdRequest, IEnumerable<GetRequestsBySupplierIdResponse>>
{
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRequestsBySupplierIdHandler> _logger;

    public GetRequestsBySupplierIdHandler(IRequestRepository requestRepository, IMapper mapper, ILogger<GetRequestsBySupplierIdHandler> logger)
    {
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetRequestsBySupplierIdResponse>> Handle(GetRequestsBySupplierIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var requestOrder = await _requestRepository.GetBySupplierId(request.SupplierId, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetRequestsBySupplierIdResponse>>(requestOrder);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving requests by supplier {SupplierId}.", request.SupplierId);
            throw;
        }
    }
}