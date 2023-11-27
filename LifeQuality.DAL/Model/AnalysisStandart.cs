namespace LifeQuality.DAL.Model;

public class AnalysisStandart : Entity
{
    public string Data { get; set; }

    public Gender Gender { get; set; }

    public Region Region { get; set; }

    public AgeRange AgeRange { get; set; }

    public HeightRange HeightRange { get; set; }

    public AnalysisType AnalysisType { get; set; }
}
