using System;
using FashionTrend.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    IMediator _mediator;

    public OrderController(IMediator mediator)
	{
        _mediator = mediator;
	}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllOrdersResponse>>>
        GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllOrdersRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<GetOrdersByStatusResponse>>
        GetByStatus(OrderStatus status, CancellationToken cancellationToken)
    {
        var request = new GetOrdersByStatusRequest(status);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("product/{productId}")]
    public async Task<ActionResult<IEnumerable<GetOrdersByProductIdResponse>>>
        GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        var request = new GetOrdersByProductIdRequest(productId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("supplier/{supplierId}")]
    public async Task<ActionResult<GetOrdersBySupplierIdResponse>>
       GetBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        var request = new GetOrdersBySupplierIdRequest(supplierId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var contract = await _mediator.Send(request);
        return Ok(contract);
    }

    [HttpPut("{orderId}/accept")]
    public async Task<ActionResult<AcceptOrderResponse>>
        AcceptRequest(Guid orderId, AcceptOrderRequest request, CancellationToken cancellationToken)
    {
        if (orderId != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{orderId}/complete")]
    public async Task<ActionResult<CompleteOrderResponse>>
        CompleteRequest(Guid orderId, CompleteOrderRequest request, CancellationToken cancellationToken)
    {
        if (orderId != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

