using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Core.StandartsInfo.Stool;
using LifeQuality.Core.StandartsInfo.Urine;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LifeQuality.Core.Services;

public class AnalysisService : IAnalysisService
{
    private readonly IDataContext _dataContext;

    public AnalysisService(IDataContext dataContext)
    {
        _dataContext = dataContext;
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

        return await query.ToListAsync();
    }

    public async Task<Analysis> GetAnalysisById(int userId)
    {
        return await _dataContext.Analyses.FirstOrDefaultAsync(a => a.Id == userId);
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

        var hemoglobinCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Hemoglobin),
            Value = analysis.Hemoglobin,
            NormalValuesRange = new() {standart.Hemoglobin.Item1, standart.Hemoglobin.Item2},
            CheckResult = GetCheckResult(analysis.Hemoglobin, standart.Hemoglobin),
        };

        var erythrocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Erythrocytes),
            Value = analysis.Erythrocytes,
            NormalValuesRange = new() {standart.Erythrocytes.Item1, standart.Erythrocytes.Item2},
            CheckResult = GetCheckResult(analysis.Erythrocytes, standart.Erythrocytes),
        };

        var leukocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Leukocytes),
            Value = analysis.Leukocytes,
            NormalValuesRange = new() {standart.Leukocytes.Item1, standart.Leukocytes.Item2},
            CheckResult = GetCheckResult(analysis.Leukocytes, standart.Leukocytes),
        };

        var plateletsCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Platelets),
            Value = analysis.Platelets,
            NormalValuesRange = new() {standart.Platelets.Item1, standart.Platelets.Item2},
            CheckResult = GetCheckResult(analysis.Platelets, standart.Platelets),
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(BloodParameters)).Length)
            { hemoglobinCheck, erythrocytesCheck, leukocytesCheck, plateletsCheck };

        return new AnalysisCheckResult { AnalysisProperties = checkResult };
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

        var phCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(UrineParameters.pH),
            Value = analysis.pH,
            NormalValuesRange = new() { standart.pH.Item1, standart.pH.Item2 },
            CheckResult = GetCheckResult(analysis.pH, standart.pH),
        };

        var proteinCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(UrineParameters.Protein),
            Value = analysis.Protein,
            NormalValuesRange = new() { standart.Protein.Item1, standart.Protein.Item2 },
            CheckResult = GetCheckResult(analysis.Protein, standart.Protein),
        };

        var glucoseCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(UrineParameters.Glucose),
            Value = analysis.Glucose,
            NormalValuesRange = new() { standart.Glucose.Item1, standart.Glucose.Item2 },
            CheckResult = GetCheckResult(analysis.Glucose, standart.Glucose),
        };

        var microorganismsCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(UrineParameters.Microorganisms),
            Value = analysis.Microorganisms,
            NormalValuesRange = new() { standart.Microorganisms.Item1, standart.Microorganisms.Item2 },
            CheckResult = GetCheckResult(analysis.Microorganisms, standart.Microorganisms),
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(UrineParameters)).Length)
            { phCheck, proteinCheck, glucoseCheck, microorganismsCheck };

        return new AnalysisCheckResult { AnalysisProperties = checkResult };
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

        var mucusCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(StoolParameters.Mucus),
            Value = analysis.Mucus,
            NormalValuesRange = new() { standart.Mucus.Item1, standart.Mucus.Item2 },
            CheckResult = GetCheckResult(analysis.Mucus, standart.Mucus),
        };

        var bloodCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(StoolParameters.Blood),
            Value = analysis.Blood,
            NormalValuesRange = new() { standart.Blood.Item1, standart.Blood.Item2 },
            CheckResult = GetCheckResult(analysis.Blood, standart.Blood),
        };

        var whiteBloodCellsCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(StoolParameters.WhiteBloodCells),
            Value = analysis.WhiteBloodCells,
            NormalValuesRange = new() { standart.WhiteBloodCells.Item1, standart.WhiteBloodCells.Item2 },
            CheckResult = GetCheckResult(analysis.WhiteBloodCells, standart.WhiteBloodCells),
        };

        var microorganismsCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(StoolParameters.Microorganisms),
            Value = analysis.Microorganisms,
            NormalValuesRange = new() { standart.Microorganisms.Item1, standart.Microorganisms.Item2 },
            CheckResult = GetCheckResult(analysis.Microorganisms, standart.Microorganisms),
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(UrineParameters)).Length)
            { mucusCheck, bloodCheck, whiteBloodCellsCheck, microorganismsCheck };

        return new AnalysisCheckResult { AnalysisProperties = checkResult };
    }

    private StoolRecord GetStoolAnalysis(Analysis analysisToCheck)
    {
        return JsonConvert.DeserializeObject<StoolRecord>(analysisToCheck.Data);
    }

    private StoolStandart GetStoolStandart(AnalysisStandart analysisStandart)
    {
        return JsonConvert.DeserializeObject<StoolStandart>(analysisStandart.Data);
    }

    private PossibleAnalysisResult GetCheckResult(double value, (double, double) normalRange)
    {
        if (value < normalRange.Item1)
        {
            return PossibleAnalysisResult.Bad;
        }

        if (value < normalRange.Item2)
        {
            return PossibleAnalysisResult.Medium;
        }

        return PossibleAnalysisResult.Good;
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
