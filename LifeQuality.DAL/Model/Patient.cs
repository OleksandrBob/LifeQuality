namespace LifeQuality.DAL.Model;

public class Patient : User
{
    public DateTime RegistrationDate { get; set; }

    public string PhoneNumber { get; set; }

    public List<Analysis> PatientAnalyses { get; set; }
}