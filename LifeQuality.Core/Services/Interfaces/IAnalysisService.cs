using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Services.Interfaces;

public interface IAnalysisService
{
    Task<List<Analysis>> GetUserAnalysis(int userId, AnalysisType analysisType = AnalysisType.None,
        bool sortByDescendingDate = false);

    Task<List<Analysis>> GetAllAnalyses();

    Task<List<AnalysisStandart>> GetAllStandarts();
}
