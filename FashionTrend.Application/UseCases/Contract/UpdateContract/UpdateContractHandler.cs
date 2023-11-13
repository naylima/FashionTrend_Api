using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class UpdateContractHandler : IRequestHandler<UpdateContractRequest, UpdateContractResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateContractHandler> _logger;

    public UpdateContractHandler(IUnitOfWork unitOfWork, IContractRepository contractRepository, IMapper mapper, ILogger<UpdateContractHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateContractResponse> Handle(UpdateContractRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var contract = await _contractRepository.Get(request.Id, cancellationToken);

            if (contract is null)
            {
                throw new InvalidOperationException("Contract not found. The provided contract does not exist.");
            }

            _mapper.Map(request, contract);

            await _unitOfWork.Commit(cancellationToken);
            _contractRepository.Update(contract);

            return _mapper.Map<UpdateContractResponse>(contract);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the contract with ID {ContractId}", request.Id);
            throw;
        }
    }

}

