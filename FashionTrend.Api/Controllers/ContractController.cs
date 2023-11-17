﻿using System;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllContractsResponse>>>
        GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllContractsRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<GetActiveContractsResponse>>>
        GetActive(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetActiveContractsRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{contractNumber}")]
    public async Task<ActionResult<GetContractResponse>>
        Get(string contractNumber, CancellationToken cancellationToken)
    {
        var request = new GetContractRequest(contractNumber);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

