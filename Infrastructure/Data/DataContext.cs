using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Hospital> Hospitals { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(h => h.RegistrationNumber);
            entity.Property(h => h.Name).IsRequired().HasMaxLength(255);
            entity.Property(h => h.MinistryCode).IsRequired().HasMaxLength(50);
            entity.Property(h => h.TerritoryCode).IsRequired().HasMaxLength(50);
            entity.Property(h => h.DistrictCode).IsRequired().HasMaxLength(50);
            entity.Property(h => h.CityCode).IsRequired().HasMaxLength(50);

            entity.HasMany(h => h.Patients)
                  .WithOne(p => p.Hospital)
                  .HasForeignKey(p => p.HospitalRegistrationNumber)
                  .HasPrincipalKey(h => h.RegistrationNumber)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.HospitalRegistrationNumber).IsRequired();
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.RecordDate).IsRequired();
            entity.Property(p => p.Disease).IsRequired();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}