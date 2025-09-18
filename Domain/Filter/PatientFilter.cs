using System.ComponentModel.DataAnnotations;

namespace Domain.Filter;

public class PatientFilter : ValidFilter
{
    [Required]
    public string HospitalRegistrationNumber { get; set; }
    [Required]
    public string Name { get; set; }
}
