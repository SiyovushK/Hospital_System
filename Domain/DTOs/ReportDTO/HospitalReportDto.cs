namespace Domain.DTOs.ReportDTO;

public class HospitalReportDto
{
    public string HospitalName { get; set; }
    public string HospitalRegistrationNumber { get; set; }
    public string MinistryCode { get; set; }
    public string TerritoryCode { get; set; }
    public string DistrictCode { get; set; }
    public string CityCode { get; set; }

    public int TotalPatientsInPeriod { get; set; }
    public int TotalPatientsFromYearStart { get; set; }
    public int RecoveredInPeriod { get; set; }
    public int RecoveredFromYearStart { get; set; }

    public decimal RecoveryPercentInPeriod => TotalPatientsInPeriod > 0 
        ? Math.Round((decimal)RecoveredInPeriod * 100 / TotalPatientsInPeriod, 2) 
        : 0;

    public decimal RecoveryPercentFromYearStart => TotalPatientsFromYearStart > 0 
        ? Math.Round((decimal)RecoveredFromYearStart * 100 / TotalPatientsFromYearStart, 2) 
        : 0;

    public int FluAndColdCount { get; set; }      // Грипп + ОРЗ
    public int TyphoidCount { get; set; }         // Брюшной тиф
    public int HepatitisCount { get; set; }       // Гепатит
    public int OtherDiseasesCount { get; set; }   // Все остальные

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
}