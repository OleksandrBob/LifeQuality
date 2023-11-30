using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace LifeQuality.Core.Services;

public class PatientService : IPatientService
{
    private readonly IDataContext _dataContext;

    public PatientService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Patient>> GetPatientsBy(int doctorId, string searchQuery)
    {
        int.TryParse(searchQuery, out var userId);

        return await _dataContext.Patients.Where(p =>
            p.DoctorId == doctorId && (
                p.Name.Contains(searchQuery) ||
                p.Surname.Contains(searchQuery) ||
                p.Id == userId ||
                p.Email.Contains(searchQuery))).ToListAsync();
    }
}
