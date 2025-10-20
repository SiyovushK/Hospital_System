using Microsoft.EntityFrameworkCore;
using Domain.DTOs.ReportDTO;
using Domain.Enums;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ReportService(DataContext context) : IReportService
{
    // 🔹 Универсальный безопасный метод для конвертации строки в enum
    private static Territories? ParseTerritorySafe(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        // Enum.TryParse с ignoreCase позволяет игнорировать регистр
        if (Enum.TryParse<Territories>(input, ignoreCase: true, out var result))
            return result;

        return null;
    }

    // 🔹 Основной метод подсчёта статистики по конкретной территории
    private async Task<HospitalReportDto> GetStatisticsByTerritoryAsync(
        Territories territory, 
        DateTime dateFrom, 
        DateTime dateTo)
    {
        var patientsQuery = context.Patients
            .Where(p => !p.IsDeleted &&
                        p.TerritoryName == territory &&
                        p.RecordDate.Date >= dateFrom.Date &&
                        p.RecordDate.Date <= dateTo.Date);

        int totalPatients = await patientsQuery.CountAsync();

        int totalRecovered = await patientsQuery
            .Where(p => p.RecoveryDate != null &&
                        p.RecoveryDate.Value.Date >= dateFrom.Date &&
                        p.RecoveryDate.Value.Date <= dateTo.Date)
            .CountAsync();

        int fluAndColdTotal = await patientsQuery
            .Where(p => p.Disease == DiseaseType.Flu || p.Disease == DiseaseType.Cold)
            .CountAsync();

        int fluAndColdRecovered = await patientsQuery
            .Where(p => (p.Disease == DiseaseType.Flu || p.Disease == DiseaseType.Cold) &&
                        p.RecoveryDate != null)
            .CountAsync();

        int typhoidTotal = await patientsQuery
            .Where(p => p.Disease == DiseaseType.Fever)
            .CountAsync();

        int typhoidRecovered = await patientsQuery
            .Where(p => p.Disease == DiseaseType.Fever && p.RecoveryDate != null)
            .CountAsync();

        int hepatitisTotal = await patientsQuery
            .Where(p => p.Disease == DiseaseType.Hepatitis)
            .CountAsync();

        int hepatitisRecovered = await patientsQuery
            .Where(p => p.Disease == DiseaseType.Hepatitis && p.RecoveryDate != null)
            .CountAsync();

        int otherDiseasesTotal = await patientsQuery
            .Where(p => p.Disease != DiseaseType.Flu &&
                        p.Disease != DiseaseType.Cold &&
                        p.Disease != DiseaseType.Fever &&
                        p.Disease != DiseaseType.Hepatitis)
            .CountAsync();

        int otherDiseasesRecovered = await patientsQuery
            .Where(p => p.Disease != DiseaseType.Flu &&
                        p.Disease != DiseaseType.Cold &&
                        p.Disease != DiseaseType.Fever &&
                        p.Disease != DiseaseType.Hepatitis &&
                        p.RecoveryDate != null)
            .CountAsync();

        return new HospitalReportDto
        {
            PatientsTotal = totalPatients,
            RecoveredTotal = totalRecovered,
            FluAndColdCountTotal = fluAndColdTotal,
            FluAndColdCountRecovered = fluAndColdRecovered,
            TyphoidCountTotal = typhoidTotal,
            TyphoidCountRecovered = typhoidRecovered,
            HepatitisCountTotal = hepatitisTotal,
            HepatitisCountRecovered = hepatitisRecovered,
            OtherDiseasesTotal = otherDiseasesTotal,
            OtherDiseasesRecovered = otherDiseasesRecovered
        };
    }

    // 🔹 Новый универсальный метод: принимает название территории (любой регистр)
    public async Task<Response<HospitalReportDto>> GetStatisticsByTerritoryNameAsync(
        string territoryName, 
        DateTime from, 
        DateTime to)
    {
        var territory = ParseTerritorySafe(territoryName);
        if (territory is null)
            return new Response<HospitalReportDto>(System.Net.HttpStatusCode.NotFound,"Территория не найдена");

        var report = await GetStatisticsByTerritoryAsync(territory.Value, from, to);
        return new Response<HospitalReportDto>(report);
    }

    // 🔹 Старые методы — тоже продолжают работать (через тот же общий метод)
    public async Task<HospitalReportDto> GetStatisticsFirdavsi(DateTime from, DateTime to)
        => await GetStatisticsByTerritoryAsync(Territories.Firdavsi, from, to);

    public async Task<HospitalReportDto> GetStatisticsShohmansur(DateTime from, DateTime to)
        => await GetStatisticsByTerritoryAsync(Territories.Shohmansur, from, to);

    public async Task<HospitalReportDto> GetStatisticsSino(DateTime from, DateTime to)
        => await GetStatisticsByTerritoryAsync(Territories.Sino, from, to);

    public async Task<HospitalReportDto> GetStatisticsSomoni(DateTime from, DateTime to)
        => await GetStatisticsByTerritoryAsync(Territories.Somoni, from, to);

    // 🔹 Сводная статистика по всем территориям
    public async Task<HospitalReport> GetStatisticsAllTerritories(DateTime from, DateTime to)
    {
        from = DateTime.SpecifyKind(from, DateTimeKind.Utc);
        to = DateTime.SpecifyKind(to, DateTimeKind.Utc);

        return new HospitalReport
        {
            PatientsTotalAllTerritories = await context.Patients
                .Where(p => p.RecordDate.Date >= from.Date)
                .CountAsync(),

            RecoveredTotalAllTerritories = await context.Patients
                .Where(p => p.RecoveryDate != null &&
                            p.RecoveryDate.Value.Date >= from.Date &&
                            p.RecoveryDate.Value.Date <= to.Date)
                .CountAsync(),

            DateFrom = from,
            DateTo = to
        };
    }
}