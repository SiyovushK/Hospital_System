using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.HospitalDTO;

public class CreateHospitalDTO
{
    [Required(ErrorMessage = "Hospital name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Ministry Name is required")]
    public string MinistryName { get; set; }

    [Required(ErrorMessage = "Territory Name is required")]
    public Territories TerritoryName { get; set; } 

    [Required(ErrorMessage = "District Name is required")]
    public string DistrictName { get; set; }

    [Required(ErrorMessage = "City Name is required")]
    public string CityName { get; set; }
}
