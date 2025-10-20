using Domain.Enums;

namespace Domain.Entities;

public class Hospital
{
    public string RegistrationNumber { get; set; } // PK
    public string Name { get; set; }
    public string? MinistryName { get; set; }
    public Territories TerritoryName { get; set; }
    public string? DistrictName { get; set; }
    public string? CityName { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Patient> Patients { get; set; }
}