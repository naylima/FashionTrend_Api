using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FashionTrend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    IMediator _mediator;

    public ProductController(IMediator mediator)
	{
        _mediator = mediator;
	}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllProductsResponse>>>
        GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetAllProductsRequest(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductResponse>>
        Get(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest(id);
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var product = await _mediator.Send(request);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProductResponse>>
        Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}

