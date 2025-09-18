using Domain.Enums;

namespace Domain.DTOs.PatientDTO;

public class CreatePatientDTO
{
    public string HospitalRegistrationNumber { get; set; }
    public string Name { get; set; }

    public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    public DiseaseType Disease { get; set; }
}