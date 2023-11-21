using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetPaymentsByDateRangeHandler : IRequestHandler<GetPaymentsByDateRangeRequest, IEnumerable<GetPaymentsByDateRangeResponse>>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPaymentsByDateRangeHandler> _logger;

    public GetPaymentsByDateRangeHandler(
        IPaymentRepository paymentRepository,
        IMapper mapper,
        ILogger<GetPaymentsByDateRangeHandler> logger)
    {
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetPaymentsByDateRangeResponse>>
        Handle(GetPaymentsByDateRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var payments = await _paymentRepository.GetByDateRange(request.StartDate, request.EndDate, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetPaymentsByDateRangeResponse>>(payments);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving payments by contract.");
            throw;
        }
    }
}