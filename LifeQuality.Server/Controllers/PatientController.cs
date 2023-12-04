using LifeQuality.Server.Comands.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LifeQuality.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery]GetPatientQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("doctor")]
    public async Task<IActionResult> GetPatientDoctor([FromQuery] GetPatientDoctorQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("analysis")]
    public async Task<IActionResult> GetPatientAnalysis([FromQuery] GetPatientAnalysis query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }
}
