using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
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
    private readonly IAnalysisService _analysisService;
    private readonly IDataContext _dataContext;

    public AnalysisController(IMediator mediator, IAnalysisService analysisService, IDataContext dataContext)
    {
        _mediator = mediator;
        _analysisService = analysisService;
        _dataContext = dataContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserAnalysis([FromQuery]GetAnalysisQuery query)
    {
        var result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<string> CreateAnalysisStandart()
    {
        BloodStandart standart = new()
        {
            Hemoglobin = (1, 2),
            Erythrocytes = (1, 2),
            Leukocytes = (1, 2),
            Platelets = (1, 2),
        };

        var json = JsonConvert.SerializeObject(standart);

        var r = JsonConvert.DeserializeObject<BloodStandart>(json);

        return json;
    }
}