using System.ComponentModel.DataAnnotations;

namespace Domain.Filter;

public class GetReportFilter
{
    [Required]
    public string HospitalRegistrationNumber { get; set; }
    public DateTime DateFrom { get; set; } = DateTime.UtcNow.AddMonths(-6);
    public DateTime DateTo { get; set; } = DateTime.UtcNow;
}