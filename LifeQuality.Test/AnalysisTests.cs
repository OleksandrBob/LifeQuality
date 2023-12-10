using CSharpFunctionalExtensions;
using LifeQuality.Core.Services;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.Core.StandartsInfo;
using LifeQuality.Core.StandartsInfo.Blood;
using LifeQuality.Core.StandartsInfo.Stool;
using LifeQuality.Core.StandartsInfo.Urine;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Migrations;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NPoco.Linq;
using System.Collections.Generic;

namespace LifeQuality.Test;

public class AnalysisTests
{
    private IAnalysisAdapter _analysisAdapter;

    [SetUp]
    public void Setup()
    {
        var mockLogger = new Mock<ILogger<AnalysisAdapter>>();
        _analysisAdapter = new AnalysisAdapter(mockLogger.Object);
    }

    [Test]
    public void CheckBloodAnalysis_ValidStringProvided_ShouldStandartizeAnalysis()
    {
        // Arrange

        var standart = new BloodStandart { Erythrocytes = (0.25, 0.45),  Hemoglobin = (0.0, 0.35),  Leukocytes = (0.0, 0.2), Platelets = (0.0, 0.65) };
        var analysis = new BloodRecord { Erythrocytes = 0.44, Hemoglobin = 0.1, Leukocytes = 0.55, Platelets = 0.51};

        var ErythrocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = "Erythrocytes",
            Value = 0.44,
            NormalValuesRange = new() { 0.25, 0.45 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var HemoglobinCheck = new AnalysisPropertyCheckResult
        {
            Name = "Hemoglobin",
            Value = 0.1,
            NormalValuesRange = new() {0, 0.35 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var LeukocytesCheck = new AnalysisPropertyCheckResult
        {
            Name = "Leukocytes",
            Value = 0.55,
            NormalValuesRange = new() { 0, 0.2 },
            CheckResult = PossibleAnalysisResult.Good,
        };

        var PlateletsCheck = new AnalysisPropertyCheckResult
        {
            Name = "Platelets",
            Value = 0.51,
            NormalValuesRange = new() { 0, 0.65 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(UrineParameters)).Length)
            { ErythrocytesCheck, HemoglobinCheck, LeukocytesCheck, PlateletsCheck };


        var expectedRecord = new AnalysisCheckResult { AnalysisProperties = checkResult };

        // Act
        var actualResult =
            _analysisAdapter.CheckBloodAnalysis(analysis, standart);

        // Assert
        Assert.That(Equals (expectedRecord.AnalysisProperties.ElementAt(0).CheckResult, actualResult.AnalysisProperties.ElementAt(0).CheckResult) 
            && Equals(expectedRecord.AnalysisProperties.ElementAt(1).CheckResult, actualResult.AnalysisProperties.ElementAt(1).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(2).CheckResult, actualResult.AnalysisProperties.ElementAt(2).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(3).CheckResult, actualResult.AnalysisProperties.ElementAt(3).CheckResult)
            );
    }

    [Test]
    public void CheckUrineAnalysis_ValidStringProvided_ShouldStandartizeAnalysis()
    {
        // Arrange

        var standart = new UrineStandart { pH = (5.0, 7.0), Glucose = (0.0, 0.1),  Microorganisms = (0.0, 0.5),  Protein = (0.0, 0.2) };
        var analysis = new UrineRecord { pH = 6.1, Glucose = 0.05, Microorganisms = 0.45, Protein = 0.25 };

        var pHCheck = new AnalysisPropertyCheckResult
        {
            Name = "pH",
            Value = 6.1,
            NormalValuesRange = new() { 5.0, 7.0 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var GlucoseCheck = new AnalysisPropertyCheckResult
        {
            Name = "Glucose",
            Value = 0.05,
            NormalValuesRange = new() { 0, 0.1 },
            CheckResult = PossibleAnalysisResult.Good,
        };

        var MicroorganismsCheck = new AnalysisPropertyCheckResult
        {
            Name = "Microorganisms",
            Value = 0.45,
            NormalValuesRange = new() { 0, 0.5 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var ProteinCheck = new AnalysisPropertyCheckResult
        {
            Name = "Protein",
            Value = 0.25,
            NormalValuesRange = new() { 0, 0.2 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(UrineParameters)).Length)
            { pHCheck, GlucoseCheck, MicroorganismsCheck, ProteinCheck };


        var expectedRecord = new AnalysisCheckResult { AnalysisProperties = checkResult };

        // Act
        var actualResult =
            _analysisAdapter.CheckUrineAnalysis(analysis, standart);

        // Assert
        Assert.That(Equals(expectedRecord.AnalysisProperties.ElementAt(0).CheckResult, actualResult.AnalysisProperties.ElementAt(0).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(1).CheckResult, actualResult.AnalysisProperties.ElementAt(1).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(2).CheckResult, actualResult.AnalysisProperties.ElementAt(2).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(3).CheckResult, actualResult.AnalysisProperties.ElementAt(3).CheckResult)
            );
    }


    [Test]
    public void CheckStoolAnalysis_ValidStringProvided_ShouldStandartizeAnalysis()
    {
        // Arrange

        var standart = new StoolStandart { Mucus = (0.25, 0.45), Blood = (0.0, 0.35), WhiteBloodCells = (0.0, 0.2), Microorganisms = (0.0, 0.65) };
        var analysis = new StoolRecord { Mucus = 0.44, Blood = 0.1, WhiteBloodCells = 0.55, Microorganisms = 0.51 };

        var mucusCheck = new AnalysisPropertyCheckResult
        {
            Name = "Mucus",
            Value = 0.44,
            NormalValuesRange = new() { 0.25, 0.45 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var bloodCheck = new AnalysisPropertyCheckResult
        {
            Name = "Blood",
            Value = 0.1,
            NormalValuesRange = new() { 0, 0.35 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        var whiteBloodCellsCheck = new AnalysisPropertyCheckResult
        {
            Name = "WhiteBloodCells",
            Value = 0.55,
            NormalValuesRange = new() { 0, 0.2 },
            CheckResult = PossibleAnalysisResult.Good,
        };

        var microorganismsCheck = new AnalysisPropertyCheckResult
        {
            Name = "Microorganisms",
            Value = 0.51,
            NormalValuesRange = new() { 0, 0.65 },
            CheckResult = PossibleAnalysisResult.Medium,
        };

        List<AnalysisPropertyCheckResult> checkResult = new(Enum.GetNames(typeof(UrineParameters)).Length)
            { mucusCheck, bloodCheck, whiteBloodCellsCheck, microorganismsCheck };


        var expectedRecord = new AnalysisCheckResult { AnalysisProperties = checkResult };

        // Act
        var actualResult =
            _analysisAdapter.CheckStoolAnalysis(analysis, standart);

        // Assert
        Assert.That(Equals(expectedRecord.AnalysisProperties.ElementAt(0).CheckResult, actualResult.AnalysisProperties.ElementAt(0).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(1).CheckResult, actualResult.AnalysisProperties.ElementAt(1).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(2).CheckResult, actualResult.AnalysisProperties.ElementAt(2).CheckResult)
            && Equals(expectedRecord.AnalysisProperties.ElementAt(3).CheckResult, actualResult.AnalysisProperties.ElementAt(3).CheckResult)
            );
    }

}