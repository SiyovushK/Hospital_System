using Domain.Enums;

namespace Domain.DTOs.HospitalDTO;

public class GetHospitalDTO
{
    public string RegistrationNumber { get; set; } // PK

    public string Name { get; set; }

    public string MinistryName { get; set; }
    public Territories TerritoryName { get; set; }
    public string DistrictName { get; set; }
    public string CityName { get; set; }
}
