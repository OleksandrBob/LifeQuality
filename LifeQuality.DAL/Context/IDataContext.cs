using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace LifeQuality.DAL.Context;

public interface IDataContext
{
    public Task CompleteAsync();
    
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }
    
    public DbSet<Analysis> Analyses { get; set; }
    
    public DbSet<AnalysisStandart> AnalysesStandarts { get; set; }
}