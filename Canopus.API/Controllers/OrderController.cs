using Microsoft.AspNetCore.Mvc;

namespace Canopus.API.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task
}