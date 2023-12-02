namespace LifeQuality.Core.StandartsInfo;

public class AnalysisCheckResult
{
    public IEnumerable<AnalysisPropertyCheckResult> AnalysisProperties { get; set; }
}

public class AnalysisPropertyCheckResult
{
    public string Name { get; set; }

    public double Value { get; set; }

    public List<double> NormalValuesRange { get; set; }

    public PossibleAnalysisResult CheckResult { get; set; }
}
