namespace Domain.DTOs.HospiralDTO;

public class GetHospitalDTO
{
    public string RegistrationNumber { get; set; } // PK

    public string Name { get; set; }

    public string MinistryCode { get; set; }
    public string TerritoryCode { get; set; }
    public string DistrictCode { get; set; }
    public string CityCode { get; set; }
}
