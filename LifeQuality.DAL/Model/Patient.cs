namespace LifeQuality.DAL.Model;

public class Patient : User
{
    public DateTime RegistrationDate { get; set; }

    public string PhoneNumber { get; set; }

    public List<Analysis> PatientAnalyses { get; set; }

    public int DoctorId { get; set; }
    
    public Doctor Doctor { get; set; }
}