using CSharpFunctionalExtensions;
using LifeQuality.Core.Services;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NPoco.Linq;
using System.Collections.Generic;

namespace LifeQuality.Test;

public class AnalysisTests
{
    private IAnalysisService _analysisService;
    private Mock<IDataContext> _mockDataContext;
    private Mock<IAnalysisAdapter> _mockAnalysisAdapter;
    private Mock<ILogger<AnalysisService>> _mockLogger;
    private List<Analysis> _mockAnalyses;

    [SetUp]
    public void Setup()
    {
       
    }


}