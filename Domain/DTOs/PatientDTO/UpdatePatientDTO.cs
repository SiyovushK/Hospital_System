namespace Domain.DTOs.PatientDTO;

public class UpdatePatientDTO : CreatePatientDTO
{
    public DateTime? DischargeDate { get; set; }
}