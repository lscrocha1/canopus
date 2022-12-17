using Canopus.API.DTOs;
using Canopus.API.Responses;
using Canopus.API.Services.Application.Customer;
using Microsoft.AspNetCore.Mvc;

namespace Canopus.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MultipleResponse<CustomerDto>>> Get(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string search = "")
    {
        var result = await _customerService.Get(
            pageIndex,
            pageSize,
            search);

        if (!result.Data.Items.Any())
            return NoContent();

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SingleResponse<CustomerDto>>> Add([FromBody] AddCustomerDto dto)
    {
        var result = await _customerService.AddCustomer(dto);

        return new ObjectResult(result)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }
}