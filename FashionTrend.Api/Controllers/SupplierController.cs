using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController : ControllerBase
{
	IMediator _mediator;

	public SupplierController(IMediator mediator)
	{
		_mediator = mediator;
	}

    [HttpGet]
    public async Task<ActionResult<List<GetAllSuppliersResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllSuppliersRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<GetSupplierByEmailResponse>>
        GetByEmail(string email, CancellationToken cancellationToken)
    {
        var request = new GetSupplierByEmailRequest(email);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSupplierRequest request)
    {
        var supplier = await _mediator.Send(request);
        return Ok(supplier);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateSupplierResponse>>
        Update(Guid id, UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}