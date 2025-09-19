using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.HospiralDTO;

public class CreateHospitalDTO
{
    [Required(ErrorMessage = "Registration Number is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Ministry code is required")]
    public string MinistryCode { get; set; }

    [Required(ErrorMessage = "Territory code is required")]
    public string TerritoryCode { get; set; }

    [Required(ErrorMessage = "District code is required")]
    public string DistrictCode { get; set; }

    [Required(ErrorMessage = "City code is required")]
    public string CityCode { get; set; }
}
