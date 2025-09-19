using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs.PatientDTO;

public class CreatePatientDTO
{
    [Required(ErrorMessage = "Hospital Registration Number is required")]
    public string HospitalRegistrationNumber { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Record Date is required")]
    public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    public DiseaseType Disease { get; set; }
}