using System;
using FashionTrend.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestController : ControllerBase
{
    IMediator _mediator;

    public RequestController(IMediator mediator)
	{
        _mediator = mediator;
	}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllRequestsResponse>>>
        GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllRequestsRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{status}")]
    public async Task<ActionResult<GetRequestsByStatusResponse>>
        GetByStatus(RequestStatus status, CancellationToken cancellationToken)
    {
        var request = new GetRequestsByStatusRequest(status);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<IEnumerable<GetRequestsByProductIdResponse>>>
        GetByProductId(Guid productId, CancellationToken cancellationToken)
    {
        var request = new GetRequestsByProductIdRequest(productId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{supplierId}")]
    public async Task<ActionResult<GetRequestsBySupplierIdResponse>>
       GetBySupplierId(Guid supplierId, CancellationToken cancellationToken)
    {
        var request = new GetRequestsBySupplierIdRequest(supplierId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestRequest request)
    {
        var contract = await _mediator.Send(request);
        return Ok(contract);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AcceptRequestResponse>>
        AcceptRequest(Guid id, AcceptRequestRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

