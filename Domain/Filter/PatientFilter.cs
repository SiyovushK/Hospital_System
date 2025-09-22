using System.ComponentModel.DataAnnotations;

namespace Domain.Filter;

public class PatientFilter : ValidFilter
{
    public string HospitalRegistrationNumber { get; set; }
    public string Name { get; set; }
}