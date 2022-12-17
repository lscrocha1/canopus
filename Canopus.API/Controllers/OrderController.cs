using Canopus.API.DTOs;
using Canopus.API.Responses;
using Canopus.API.Services.Application.Order;
using Microsoft.AspNetCore.Mvc;

namespace Canopus.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SingleResponse<CustomerOrderDto>>> GetOrdersByCustomerId(
        [FromRoute] Guid customerId,
        CancellationToken token)
    {
        var result = await _orderService.GetCustomerOrders(customerId, token);

        if (result.Data is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add(
        [FromRoute] Guid customerId,
        [FromBody] AddOrderDto dto,
        CancellationToken token)
    {
        await _orderService.Add(customerId, dto, token);

        return StatusCode(StatusCodes.Status201Created);
    }
}