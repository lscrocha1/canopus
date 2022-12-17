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

    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SingleResponse<CustomerOrderDto>>> GetCustomerOrders(
        [FromRoute] Guid customerId)
    {
        var result = await _orderService.GetCustomerOrder(customerId);

        if (result.Data is null)
            return NotFound();

        return Ok(result);
    }
}