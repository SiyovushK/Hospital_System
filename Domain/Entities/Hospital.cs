namespace Domain.Entities;

public class Hospital
{
    public string RegistrationNumber { get; set; } // PK

    public string Name { get; set; }

    public string MinistryCode { get; set; }
    public string TerritoryCode { get; set; }
    public string DistrictCode { get; set; }
    public string CityCode { get; set; }
    
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Patient> Patients { get; set; }
}