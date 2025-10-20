using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.PatientDTO;

public class UpdatePatientDTO : CreatePatientDTO
{
    [Required(ErrorMessage = "Territory Name is required")]
    public Territories TerritoryName { get; set; }
    public DateTime? RecoveryDate { get; set; }
}