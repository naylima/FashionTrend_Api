using System;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateMaterialHandler : IRequestHandler<CreateMaterialRequest, CreateMaterialResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMaterialRepository _materialRepository;
	private readonly IMapper _mapper;
    private readonly ILogger<CreateMaterialHandler> _logger;

    public CreateMaterialHandler(
        IUnitOfWork unitOfWork,
        IMaterialRepository materialRepository,
        IMapper mapper,
        ILogger<CreateMaterialHandler> logger)
	{
		_unitOfWork = unitOfWork;
        _materialRepository = materialRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<CreateMaterialResponse> Handle(CreateMaterialRequest request, CancellationToken cancellationToken)
	{
        try
        {
            var existingMaterial = await _materialRepository.GetByName(request.Name, cancellationToken);

            if (existingMaterial is not null)
            {
                throw new InvalidOperationException("The provided material name is already registered.");
            }

            var material = _mapper.Map<Material>(request);
            _materialRepository.Create(material);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateMaterialResponse>(material);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new material with name {MaterialName}", request.Name);
            throw;
        }
    }
}
