namespace Domain.DTOs.HospiralDTO;

public class CreateHospitalDTO
{
    public string Name { get; set; }

    public string MinistryCode { get; set; }
    public string TerritoryCode { get; set; }
    public string DistrictCode { get; set; }
    public string CityCode { get; set; }
}
