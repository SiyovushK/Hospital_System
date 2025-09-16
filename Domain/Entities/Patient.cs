using Domain.Enums;

namespace Domain.Entities;

public class Patient
{
    public int Id { get; set; }

    public string HospitalRegistrationNumber { get; set; }
    public string Name { get; set; }

    public DateTime RecordDate { get; set; } = DateTime.UtcNow;
    public DateTime? DischargeDate { get; set; }
    public DiseaseType Disease { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public virtual Hospital Hospital { get; set; }
}