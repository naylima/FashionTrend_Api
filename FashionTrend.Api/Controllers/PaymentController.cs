using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    IMediator _mediator;

    public PaymentController(IMediator mediator)
	{
        _mediator = mediator;
	}

    [HttpGet("{contractId}")]
    public async Task<ActionResult<IEnumerable<GetPaymentsByContractIdResponse>>>
        GetAllByContractId(Guid contractId, CancellationToken cancellationToken)
    {
        var request = new GetPaymentsByContractIdRequest(contractId);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetPaymentsByDateRangeResponse>>>
         GetAllByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
    {
        var request = new GetPaymentsByDateRangeRequest(startDate, endDate);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePaymentRequest request)
    {
        var payment = await _mediator.Send(request);
        return Ok(payment);
    }
}

