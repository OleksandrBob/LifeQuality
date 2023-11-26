using LifeQuality.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace LifeQuality.DAL.Context;

public sealed class DataContext : DbContext, IDataContext
{
    private IConnectionOptions _options;

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients { get; set; }
    
    public DbSet<Analysis> Analyses { get; set; }

    public DbSet<AnalysisStandart> AnalysesStandarts { get; set; }

    public DataContext()
    {
    }

    public DataContext(IConnectionOptions options)
    {
        _options = options;
        ChangeTracker.AutoDetectChangesEnabled = true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=LifeQuality;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
