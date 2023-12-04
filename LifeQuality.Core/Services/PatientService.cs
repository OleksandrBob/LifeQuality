using CSharpFunctionalExtensions;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LifeQuality.Core.Services;

public class PatientService : IPatientService
{
    private readonly IDataContext _dataContext;
    private readonly ILogger _logger;

    public PatientService(IDataContext dataContext, ILogger<PatientService> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<List<Patient>> GetPatientsBy(int doctorId, string searchQuery)
    {
        int.TryParse(searchQuery, out var userId);
        var result = await _dataContext.Patients.Where(p =>
            p.DoctorId == doctorId && (
                p.Name.Contains(searchQuery) ||
                p.Surname.Contains(searchQuery) ||
                p.Id == userId ||
                p.Email.Contains(searchQuery))).ToListAsync();
     
        _logger.LogInformation($"Gotten count - {result.Count()} patients for doctor {doctorId}");

        return result;
    }

    public async Task<List<Analysis>> GetPatientAnalysis(int patientId)
    {
        var result = _dataContext.Analyses.Where(p =>
            p.PatientId == patientId);

        _logger.LogInformation($"Gotten count - {result.Count()} analysis for doctor {patientId}");

        return await result.ToListAsync();
    }

    public async Task<Doctor> GetPatientDoctor(int patientId)
    {
        var patient = await _dataContext.Patients.FirstAsync(p => p.Id == patientId);
        var result = await _dataContext.Doctors.FirstAsync(d => d.Id == patient.DoctorId);

        _logger.LogInformation($"Gotten id {result.Id} doctor for patient {patientId}");

        return result;
    }

}
