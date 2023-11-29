using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.DAL.Model;
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

    public AnalysisController(IMediator mediator, IAnalysisService analysisService)
    {
        _mediator = mediator;
        _analysisService = analysisService;
    }

    [HttpGet]
    public async Task<List<AnalysisStandart>> GetAllAnalysis()
    {
        var r = await _analysisService.GetAllStandarts();
        var rr = JsonConvert.DeserializeObject<BloodStandart>(r[0].Data);

        return r;
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