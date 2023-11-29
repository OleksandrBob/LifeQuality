using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Services.Interfaces;

public interface IAnalysisService
{
    Task<List<Analysis>> GetAllAnalyses();
    
    Task<List<AnalysisStandart>> GetAllStandarts();
}