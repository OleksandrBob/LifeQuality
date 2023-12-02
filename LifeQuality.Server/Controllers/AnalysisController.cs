using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Server.Comands.Analysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LifeQuality.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnalysisController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAnalysis([FromQuery] GetAnalysisQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
    
    [HttpGet("checked")]
    public async Task<IActionResult> GetAnalysisChecked([FromQuery] GetAnalysisCheckedQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}
