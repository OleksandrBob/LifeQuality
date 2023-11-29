using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Services.Interfaces;

public interface IPatientService
{
    Task<List<Patient>> GetPatientsBy(string searchQuery);
}