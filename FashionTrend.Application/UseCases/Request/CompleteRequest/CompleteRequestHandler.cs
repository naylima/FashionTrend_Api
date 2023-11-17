using System;
using System.Threading;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CompleteRequestHandler : IRequestHandler<CompleteRequestRequest, CompleteRequestResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestRepository _requestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CompleteRequestHandler> _logger;

    public CompleteRequestHandler(
        IUnitOfWork unitOfWork,
        IRequestRepository requestRepository,
        IMapper mapper,
        ILogger<CompleteRequestHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _requestRepository = requestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CompleteRequestResponse> Handle(CompleteRequestRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var requestOrder = await _requestRepository.Get(request.Id, cancellationToken);
            if (requestOrder is null)
            {
                throw new InvalidOperationException("Request not found. The provided request does not exist.");
            }

            requestOrder.Status = RequestStatus.Completed;

            _mapper.Map(request, requestOrder);
            _requestRepository.Update(requestOrder);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CompleteRequestResponse>(requestOrder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the request with ID {RequestId}", request.Id);
            throw;
        }
    }
}

