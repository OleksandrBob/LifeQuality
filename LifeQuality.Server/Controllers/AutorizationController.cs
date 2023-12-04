using LifeQuality.Server.Comands.Authorization;
using LifeQuality.Server.Comands.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeQuality.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AutorizationController: ControllerBase
{
    private readonly IMediator _mediator;
    public AutorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("login")]
    public async Task<IActionResult> LogIn([FromQuery] LogInData query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}
