using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Dto;

public class AnalysisDto
{
    public int Id { get; set; }

    public string LaboratoryName { get; set; }

    public AnalysisType AnalysisType { get; set; }

    public int PatientId { get; set; }

    public DateTime AnalysisDate { get; set; }
}