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
}