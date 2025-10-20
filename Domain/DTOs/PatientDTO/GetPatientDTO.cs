using Domain.Enums;

namespace Domain.DTOs.PatientDTO;

public class GetPatientDTO
{
    public int Id { get; set; }

    public string HospitalRegistrationNumber { get; set; }
    public string Name { get; set; }

    public Territories TerritoryName { get; set; }
    public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    public DateTime? RecoveryDate { get; set; }
    public DiseaseType Disease { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}
