using System;
using FashionTrend.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController : ControllerBase
{
	IMediator _mediator;

    public ContractController(IMediator mediator)
	{
		_mediator = mediator;
	}

    [HttpGet("{contractNumber}")]
    public async Task<ActionResult<GetContractResponse>>
        Get(string contractNumber, CancellationToken cancellationToken)
    {
        var request = new GetContractRequest(contractNumber);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("status/{contractStatus}")]
    public async Task<ActionResult<IEnumerable<GetContractsByStatusResponse>>>
        GetByStatus(ContractStatus contractStatus, CancellationToken cancellationToken)
    {
        var request = new GetContractsByStatusRequest(contractStatus);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

