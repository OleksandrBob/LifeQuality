namespace LifeQuality.DAL.Model;

public class Doctor : User
{
    public DateTime AccountCreationDate { get; set; }

    public List<Patient> Patients { get; set; }
}