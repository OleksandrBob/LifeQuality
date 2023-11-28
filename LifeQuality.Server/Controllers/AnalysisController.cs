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
    public async Task<List<Analysis>> GetAllAnalysis()
    {
        var r = await _analysisService.GetAllAnalyses();
        return r;
    }

    [HttpPost]
    public async Task<string> CreateAnalysisStandart()
    {
        BloodStandart standart = new()
        {
            Hemoglobin = new() { (0, 4), (4.01, 5), (5.01, double.MaxValue) },
            Erythrocytes = new() { (0, 4), (4.01, 5), (5.01, double.MaxValue) },
            Leukocytes = new() { (0, 4), (4.01, 5), (5.01, double.MaxValue) },
            Platelets = new() { (0, 4), (4.01, 5), (5.01, double.MaxValue) },
        };

        var json = JsonConvert.SerializeObject(standart);
        return json;
    }
}