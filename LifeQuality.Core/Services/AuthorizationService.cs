using LifeQuality.Core.Dto;
using LifeQuality.Core.Services.Interfaces;
using LifeQuality.DAL.Context;
using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeQuality.Core.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IDataContext _dataContext;
    private readonly ILogger _logger;

    public AuthorizationService(IDataContext dataContext, ILogger<PatientService> logger)
    {
        _dataContext = dataContext;
        _logger = logger;
    }

    public async Task<LoginResultDto> LogIn(string login, string password)
    {
        var doctor = await _dataContext.Doctors.Where(d => d.Email == login && d.Password == password).ToListAsync();
        if(doctor.Count == 0)
        {
            var patient = await _dataContext.Patients.Where(p => p.Email == login && p.Password == password).ToListAsync();
            if (patient.Count == 0) return null;
            _logger.LogInformation($"Login {patient.First().Id} patient");
            return new LoginResultDto() { Id = patient.First().Id, Status = "patient" };
        }
        _logger.LogInformation($"Login {doctor.First().Id} doctor");
        return new LoginResultDto() { Id = doctor.First().Id, Status = "doctor" };
    }
}