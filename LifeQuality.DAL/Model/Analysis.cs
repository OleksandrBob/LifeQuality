namespace LifeQuality.DAL.Model;

public class Analysis : Entity
{
    public string LaboratoryName { get; set; }
    
    public AnalysisType AnalysisType { get; set; }

    public int PatientId { get; set; }

    public Patient Patient { get; set; }

    public string Data { get; set; }

    public DateTime AnalysisDate { get; set; }
}