using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterialController : ControllerBase
{
    IMediator _mediator;

    public MaterialController(IMediator mediator)
	{
        _mediator = mediator;
	}

    [HttpGet]
    public async Task<ActionResult<List<GetAllMaterialsResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllMaterialsRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMaterialResponse>>
        Get(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetMaterialRequest(id);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMaterialRequest request)
    {
        var material = await _mediator.Send(request);
        return Ok(material);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateMaterialResponse>>
        Update(Guid id, UpdateMaterialRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

