using System;
using AutoMapper;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class UpdateMaterialHandler : IRequestHandler<UpdateMaterialRequest, UpdateMaterialResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateMaterialHandler> _logger;

    public UpdateMaterialHandler(IUnitOfWork unitOfWork, IMaterialRepository materialRepository, IMapper mapper, ILogger<UpdateMaterialHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _materialRepository = materialRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateMaterialResponse> Handle(UpdateMaterialRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var material = await _materialRepository.Get(request.Id, cancellationToken);

            if (material is null)
            {
                throw new InvalidOperationException("Material not found. The provided material does not exist.");
            }

            _mapper.Map(request, material);
            _materialRepository.Update(material);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateMaterialResponse>(material);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the material with ID {MaterialId}", request.Id);
            throw;
        }
    }

}

