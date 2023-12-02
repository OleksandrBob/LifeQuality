using LifeQuality.Core.StandartsInfo.Interfaces;


public class BloodRecord : IRecord
{
    public double Hemoglobin { get; set; }

    public double Erythrocytes { get; set; }

    public double Platelets { get; set; }

    public double Leukocytes { get; set; }
}
