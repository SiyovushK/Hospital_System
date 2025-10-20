namespace Domain.DTOs.ReportDTO;

public class HospitalReportDto
{
    public int PatientsTotal { get; set; }
    public int RecoveredTotal { get; set; }

    public int FluAndColdCountTotal { get; set; }      // Грипп + ОРЗ
    public int FluAndColdCountRecovered { get; set; }      // Грипп + ОРЗ

    public int TyphoidCountTotal { get; set; }         // Брюшной тиф
    public int TyphoidCountRecovered { get; set; }         // Брюшной тиф

    public int HepatitisCountTotal { get; set; }       // Гепатит
    public int HepatitisCountRecovered { get; set; }       // Гепатит

    public int OtherDiseasesTotal { get; set; }   // Все остальные
    public int OtherDiseasesRecovered { get; set; }   // Все остальные

    public decimal RecoveryPercentInPeriod => PatientsTotal > 0
        ? Math.Round((decimal)RecoveredTotal * 100 / PatientsTotal, 2)
        : 0;
    public decimal FluAndColdCountPercent => FluAndColdCountTotal > 0
        ? Math.Round((decimal)FluAndColdCountRecovered * 100 / FluAndColdCountTotal, 2)
        : 0;
    public decimal TyphoidCountPercent => TyphoidCountTotal > 0
        ? Math.Round((decimal)HepatitisCountRecovered * 100 / TyphoidCountTotal, 2)
        : 0;
    public decimal HepatitisCountPercent => HepatitisCountTotal > 0
        ? Math.Round((decimal)HepatitisCountRecovered * 100 / HepatitisCountTotal, 2)
        : 0;
    public decimal OtherDiseasesPercent => OtherDiseasesTotal > 0
        ? Math.Round((decimal)OtherDiseasesRecovered * 100 / OtherDiseasesTotal, 2)
        : 0;
}   