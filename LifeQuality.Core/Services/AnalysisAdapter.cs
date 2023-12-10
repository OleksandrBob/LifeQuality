using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Core.StandartsInfo.Stool;
using LifeQuality.Core.StandartsInfo.Urine;
using LifeQuality.DAL.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.Services;

public class AnalysisAdapter : IAnalysisAdapter
{
    private readonly ILogger _logger;

    public AnalysisAdapter(ILogger<AnalysisAdapter> logger)
    {
        _logger = logger;
    }

    public AnalysisCheckResult CheckBloodAnalysis(BloodRecord analysis, BloodStandart standart)
    {
        var hemoglobinCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Hemoglobin),
            Value = analysis.Hemoglobin,
            NormalValuesRange = new() { standart.Hemoglobin.Item1, standart.Hemoglobin.Item2 },
            CheckResult = GetCheckResult(analysis.Hemoglobin, standart.Hemoglobin),
        };

        var erythrocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Erythrocytes),
            Value = analysis.Erythrocytes,
            NormalValuesRange = new() { standart.Erythrocytes.Item1, standart.Erythrocytes.Item2 },
            CheckResult = GetCheckResult(analysis.Erythrocytes, standart.Erythrocytes),
        };

        var leukocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Leukocytes),
            Value = analysis.Leukocytes,
            NormalValuesRange = new() { standart.Leukocytes.Item1, standart.Leukocytes.Item2 },
            CheckResult = GetCheckResult(analysis.Leukocytes, standart.Leukocytes),
        };

        var plateletsCheck = new AnalysisPropertyCheckResult
        {
            Name = nameof(BloodParameters.Platelets),
            Value = analysis.Platelets,
            NormalValuesRange = new() { standart.Platelets.Item1, standart.Platelets.Item2 },
            CheckResult = GetCheckResult(analysis.Platelets, standart.Platelets),
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(BloodParameters)).Length)
            { hemoglobinCheck, erythrocytesCheck, leukocytesCheck, plateletsCheck };

        _logger.LogInformation($"Analysis of blood has been standartized");
        return new AnalysisCheckResult { AnalysisProperties = checkResult };

        return new AnalysisCheckResult(); 
    }

    public AnalysisCheckResult CheckUrineAnalysis(UrineRecord analysis, UrineStandart standart)
    {
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

        _logger.LogInformation($"Analysis of urine has been standartized");

        return new AnalysisCheckResult { AnalysisProperties = checkResult };
    }

    public AnalysisCheckResult CheckStoolAnalysis(StoolRecord analysis, StoolStandart standart)
    {
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

        _logger.LogInformation($"Analysis of stool has been standartized");

        return new AnalysisCheckResult { AnalysisProperties = checkResult };
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
}
