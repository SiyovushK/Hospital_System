using System.Net;
using Domain.DTOs.ReportDTO;
using Domain.Enums;
using Domain.Filter;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ReportService(DataContext context)
{
    public async Task<Response<HospitalReportDto>> GetHospitalReportAsync(GetReportFilter reportFilter)
    {
        if (reportFilter.DateFrom > reportFilter.DateTo
            || reportFilter.DateTo > DateTime.UtcNow)
            return new Response<HospitalReportDto>(HttpStatusCode.BadRequest, "Invalid date range");

        var hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.RegistrationNumber == reportFilter.HospitalRegistrationNumber && !h.IsDeleted);
        if (hospital == null)
            return new Response<HospitalReportDto>(HttpStatusCode.BadRequest, "Hospital not found");

        var startYear = new DateTime(DateTime.UtcNow.Year, 1, 1);
        var now = DateTime.UtcNow;

        var hospitalReportDto = new HospitalReportDto
        {
            HospitalName = hospital.Name,
            HospitalRegistrationNumber = hospital.RegistrationNumber,
            MinistryCode = hospital.MinistryCode,
            TerritoryCode = hospital.TerritoryCode,
            DistrictCode = hospital.DistrictCode,
            CityCode = hospital.CityCode,

            TotalPatientsInPeriod = await context.Patients
                .Where(p => p.RecordDate >= reportFilter.DateFrom && p.RecordDate <= reportFilter.DateTo)
                .CountAsync(),
            TotalPatientsFromYearStart = await context.Patients
                .Where(p => p.RecordDate >= startYear && p.RecordDate <= now)
                .CountAsync(),
            RecoveredInPeriod = await context.Patients
                .Where(p => p.RecordDate >= reportFilter.DateFrom && p.RecordDate <= reportFilter.DateTo
                        && p.DischargeDate <= reportFilter.DateTo && p.DischargeDate >= reportFilter.DateFrom)
                .CountAsync(),
            RecoveredFromYearStart = await context.Patients
                .Where(p => p.RecordDate >= startYear && p.RecordDate <= now
                        && p.DischargeDate >= startYear && p.DischargeDate <= now)
                .CountAsync(),

            FluAndColdCount = await context.Patients
                .Where(p => p.Disease == DiseaseType.Грипп || p.Disease == DiseaseType.ОРЗ).CountAsync(),
            TyphoidCount = await context.Patients
                .Where(p => p.Disease == DiseaseType.БрюшнойТиф).CountAsync(),
            HepatitisCount = await context.Patients
                .Where(p => p.Disease == DiseaseType.Гепатит).CountAsync(),
            OtherDiseasesCount = await context.Patients
                .Where(p => p.Disease != DiseaseType.Грипп
                        && p.Disease != DiseaseType.ОРЗ
                        && p.Disease != DiseaseType.БрюшнойТиф
                        && p.Disease != DiseaseType.Гепатит)
                .CountAsync(),

            PeriodStart = reportFilter.DateFrom,
            PeriodEnd = reportFilter.DateTo
        };
        
        return new Response<HospitalReportDto>(hospitalReportDto);
    }
}