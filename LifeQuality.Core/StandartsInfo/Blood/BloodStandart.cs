namespace LifeQuality.Core.StandartsInfo.Blood;

public class BloodStandart
{
    public List<(double, double)> Hemoglobin { get; set; }

    public List<(double, double)> Erythrocytes { get; set; }

    public List<(double, double)> Platelets { get; set; }

    public List<(double, double)> Leukocytes { get; set; }
}
