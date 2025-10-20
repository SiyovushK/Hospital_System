using Domain.DTOs.ReportDTO;
using Domain.Response;

namespace Infrastructure.Interfaces;

public interface IReportService
{
    Task<HospitalReportDto> GetStatisticsFirdavsi(DateTime dateFrom, DateTime dateTo);
    Task<HospitalReportDto> GetStatisticsSomoni(DateTime dateFrom, DateTime dateTo);
    Task<HospitalReportDto> GetStatisticsShohmansur(DateTime dateFrom, DateTime dateTo);
    Task<HospitalReportDto> GetStatisticsSino(DateTime dateFrom, DateTime dateTo);
    Task<HospitalReport> GetStatisticsAllTerritories(DateTime dateFrom, DateTime dateTo);
}
