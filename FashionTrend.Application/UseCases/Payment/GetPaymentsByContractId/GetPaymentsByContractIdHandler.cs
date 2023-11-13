using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetPaymentsByContractIdHandler : IRequestHandler<GetPaymentsByContractIdRequest, IEnumerable<GetPaymentsByContractIdResponse>>
{
    private readonly IContractRepository _contractRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPaymentsByContractIdHandler> _logger;

    public GetPaymentsByContractIdHandler(
        IContractRepository contractRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper,
        ILogger<GetPaymentsByContractIdHandler> logger)
    {
        _contractRepository = contractRepository;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<GetPaymentsByContractIdResponse>>
        Handle(GetPaymentsByContractIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contract = await _contractRepository.Get(request.ContractId, cancellationToken);

            if (contract == null)
            {
                throw new InvalidOperationException("Contract not found.");
            }

            var payments = await _paymentRepository.GetByContractId(request.ContractId, cancellationToken);

            var response = _mapper.Map<IEnumerable<GetPaymentsByContractIdResponse>>(payments);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving payments by contract.");
            throw;
        }
    }
}