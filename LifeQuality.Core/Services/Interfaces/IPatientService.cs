using LifeQuality.DAL.Model;

namespace LifeQuality.Core.Services.Interfaces;

public interface IPatientService
{
    Task<List<Patient>> GetPatientsBy(int doctorId, string searchQuery);
    Task<List<Analysis>> GetPatientAnalysis(int patientId);
    Task<Doctor> GetPatientDoctor(int patientId);
}