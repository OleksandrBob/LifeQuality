using LifeQuality.Core.StandartsInfo;
using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Services.Interfaces;

public interface IAnalysisService
{
    Task<List<Analysis>> GetUserAnalysis(int userId, AnalysisType analysisType = AnalysisType.None,
        bool sortByDescendingDate = false);

    Task<Analysis> GetAnalysisById(int userId);

    Task<AnalysisStandart> GetStandartByParameters(
        AnalysisType type,
        AgeRange ageRange,
        Gender gender,
        HeightRange heightRange,
        Region region);

    Task<List<Analysis>> GetAllAnalyses();

    Task<List<AnalysisStandart>> GetAllStandarts();

    AnalysisCheckResult CheckAnalysisDueToStandart(Analysis analysisToCheck, AnalysisStandart analysisStandart);
    Task<int> AddNewAnalysis(int patientId, string laboratoryName, AnalysisType analysisType, DateTime analysisDate, string data);
}
