using CSharpFunctionalExtensions;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Core.StandartsInfo.Stool;
using LifeQuality.Core.StandartsInfo.Urine;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LifeQuality.Core.Services;

public class AnalysisService : IAnalysisService
{
    private readonly IDataContext _dataContext;
    private readonly IAnalysisAdapter _analysisAdapter;
    private readonly ILogger _logger;
    public AnalysisService(IDataContext dataContext, IAnalysisAdapter analysisAdapter, ILogger<AnalysisService> logger)
    {
        _dataContext = dataContext;
        _analysisAdapter = analysisAdapter;
        _logger = logger;
    }

    public async Task<List<Analysis>> GetUserAnalysis(int userId, AnalysisType analysisType = AnalysisType.None,
        bool sortByDescendingDate = false)
    {
        var query = _dataContext.Analyses.Where(a => a.PatientId == userId);

        if (analysisType != AnalysisType.None)
        {
            query = query.Where(a => a.AnalysisType == analysisType);
        }

        query = sortByDescendingDate
            ? query.OrderByDescending(a => a.AnalysisDate)
            : query.OrderBy(a => a.AnalysisDate);

        _logger.LogInformation($"Gotten count - {query.Count()} analysis for user {userId}");
        return await query.ToListAsync();
    }

    public async Task<Analysis> GetAnalysisById(int userId)
    {
        var result = await _dataContext.Analyses.FirstOrDefaultAsync(a => a.Id == userId);
        if(result != null)
           _logger.LogInformation($"Gotten - {result.Id} analysis for user {userId}");
        else
            _logger.LogInformation($"Gotten 0 analysis for user {userId}");
        return result;
    }

    public async Task<AnalysisStandart> GetStandartByParameters(
        AnalysisType type,
        AgeRange ageRange,
        Gender gender,
        HeightRange heightRange,
        Region region)
    {
        var result = await _dataContext.AnalysesStandarts.FirstAsync(s =>
            s.AnalysisType == type &&
            s.Region == region &&
            s.AgeRange == ageRange &&
            s.HeightRange == heightRange &&
            s.Gender == gender);
        _logger.LogInformation($"Gotten id - {result.Id} standart");
        return result;
    }

    public AnalysisCheckResult CheckAnalysisDueToStandart(Analysis analysisToCheck, AnalysisStandart analysisStandart)
    {
        return analysisStandart.AnalysisType switch
        {
            AnalysisType.Blood => CheckBlood(analysisToCheck, analysisStandart),
            AnalysisType.Urine => CheckUrine(analysisToCheck, analysisStandart),
            AnalysisType.Stool => CheckStool(analysisToCheck, analysisStandart),
        };
    }

    private AnalysisCheckResult CheckBlood(Analysis analysisToCheck, AnalysisStandart analysisStandart)
    {
        var standart = GetBloodStandart(analysisStandart);
        var analysis = GetBloodAnalysis(analysisToCheck);

        return _analysisAdapter.CheckBloodAnalysis(analysis, standart);
    }

    private BloodRecord GetBloodAnalysis(Analysis analysisToCheck)
    {
        return JsonConvert.DeserializeObject<BloodRecord>(analysisToCheck.Data);
    }

    private BloodStandart GetBloodStandart(AnalysisStandart analysisStandart)
    {
        return JsonConvert.DeserializeObject<BloodStandart>(analysisStandart.Data);
    }

    private AnalysisCheckResult CheckUrine(Analysis analysisToCheck, AnalysisStandart analysisStandart)
    {
        var standart = GetUrineStandart(analysisStandart);
        var analysis = GetUrineAnalysis(analysisToCheck);

        return _analysisAdapter.CheckUrineAnalysis(analysis, standart);
    }

    private UrineRecord GetUrineAnalysis(Analysis analysisToCheck)
    {
        return JsonConvert.DeserializeObject<UrineRecord>(analysisToCheck.Data);
    }

    private UrineStandart GetUrineStandart(AnalysisStandart analysisStandart)
    {
        return JsonConvert.DeserializeObject<UrineStandart>(analysisStandart.Data);
    }

    private AnalysisCheckResult CheckStool(Analysis analysisToCheck, AnalysisStandart analysisStandart)
    {
        var standart = GetStoolStandart(analysisStandart);
        var analysis = GetStoolAnalysis(analysisToCheck);

        return _analysisAdapter.CheckStoolAnalysis(analysis, standart);
    }

    private StoolRecord GetStoolAnalysis(Analysis analysisToCheck)
    {
        return JsonConvert.DeserializeObject<StoolRecord>(analysisToCheck.Data);
    }

    private StoolStandart GetStoolStandart(AnalysisStandart analysisStandart)
    {
        return JsonConvert.DeserializeObject<StoolStandart>(analysisStandart.Data);
    }


    public async Task<List<Analysis>> GetAllAnalyses()
    {
        return await _dataContext.Analyses.ToListAsync();
    }

    public async Task<List<AnalysisStandart>> GetAllStandarts()
    {
        return await _dataContext.AnalysesStandarts.ToListAsync();
    }
}
