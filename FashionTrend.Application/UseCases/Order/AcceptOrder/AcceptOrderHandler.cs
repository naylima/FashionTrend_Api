using System;
using System.Threading;
using AutoMapper;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Enums;
using FashionTrend.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class AcceptOrderHandler : IRequestHandler<AcceptOrderRequest, AcceptOrderResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IContractRepository _contractRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AcceptOrderHandler> _logger;

    public AcceptOrderHandler(
        IUnitOfWork unitOfWork,
        IContractRepository contractRepository,
        IOrderRepository orderRepository,
        ISupplierRepository supplierRepository,
        IMapper mapper,
        ILogger<AcceptOrderHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _orderRepository = orderRepository;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AcceptOrderResponse> Handle(AcceptOrderRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var supplier = await ValidateSupplier(request.SupplierId, cancellationToken);

            var order = await _orderRepository.Get(request.Id, cancellationToken);
            if (order is null)
            {
                throw new InvalidOperationException("Request not found. The provided order does not exist.");
            }

            bool hasMaterials = await SupplierHasMaterials(supplier.Id, order.ProductId, cancellationToken);
            if (!hasMaterials)
            {
                throw new InvalidOperationException("Supplier does not have the required materials for the product.");
            }

            var contract = await ValidateOrCreateContract(supplier.Id, cancellationToken);

            order.ContractId = contract.Id;
            order.SupplierId = supplier.Id;
            order.Status = OrderStatus.Accepted;

            _mapper.Map(request, order);
            _orderRepository.Update(order);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<AcceptOrderResponse>(order);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the order with ID {RequestId}", request.Id);
            throw;
        }
    }

    private async Task<Supplier> ValidateSupplier(Guid supplierId, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.Get(supplierId, cancellationToken);
        if (supplier is null)
        {
            throw new InvalidOperationException("Supplier not found. The provided supplier does not exist.");
        }

        return supplier;
    }

    private async Task<bool> SupplierHasMaterials(Guid supplierId, Guid productId, CancellationToken cancellationToken)
    {
        return await _supplierRepository.SupplierHasMaterials(supplierId, productId, cancellationToken);
    }

    private async Task<Contract> ValidateOrCreateContract(Guid supplierId, CancellationToken cancellationToken)
    {
        var existingContract = await _contractRepository.GetActiveContractBySupplierId(supplierId, cancellationToken);

        if (existingContract == null)
        {
            var newContract = new Contract
            {
                ContractNumber = await GenerateContractNumberAsync(cancellationToken),
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddYears(1),
                Status = ContractStatus.Active,
                SupplierId = supplierId,
            };

            _contractRepository.Create(newContract);
            await _unitOfWork.Commit(cancellationToken);

            var notificationHandler = new CreateNotificationHandler(
               "ACd88dca04cf4552c810ba6f3b47a7a802",
               "73fcbc3a5470273c768a015ce175fbc1",
               "+12678340756");

            notificationHandler.SendSMS("+5519982220048", "Hello, we have a new contract draft available.");

            return newContract;
        }

        return existingContract;
    }

    private async Task<string> GenerateContractNumberAsync(CancellationToken cancellationToken)
    {
        var random = new Random();
        var contractNumber = random.Next(10000, 100000).ToString();

        var existingContract = await _contractRepository.GetByContractNumber(contractNumber, cancellationToken);

        if (existingContract is not null)
        {
            throw new InvalidOperationException("The provided contract number is already registered.");
        }

        return contractNumber;
    }
}

