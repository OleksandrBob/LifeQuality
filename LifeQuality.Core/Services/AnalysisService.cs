using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace LifeQuality.Core.Services;

public class AnalysisService : IAnalysisService
{
    private readonly IDataContext _dataContext;

    public AnalysisService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Analysis>> GetAllAnalyses()
    {
        return await _dataContext.Analyses.ToListAsync();
    }

    public async Task<List<AnalysisStandart>> GetAllStandarts()
    {
        return await _dataContext.AnalysesStandarts.ToListAsync();
    }

    public async Task<AnalysisStandart> GetStandartByParameters(
        AnalysisType type,
        AgeRange ageRange,
        Gender gender,
        HeightRange heightRange,
        Region region)
    {
        return await _dataContext.AnalysesStandarts.FirstAsync(s =>
            s.AnalysisType == type &&
            s.Region == region &&
            s.AgeRange == ageRange &&
            s.HeightRange == heightRange &&
            s.Gender == gender);
    }
}
