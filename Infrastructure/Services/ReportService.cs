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
        var dateFromUtc = DateTime.SpecifyKind(reportFilter.DateFrom, DateTimeKind.Utc);
        var dateToUtc = DateTime.SpecifyKind(reportFilter.DateTo, DateTimeKind.Utc);

        if (dateFromUtc > dateToUtc || dateToUtc > DateTime.UtcNow)
            return new Response<HospitalReportDto>(HttpStatusCode.BadRequest, "Invalid date range");

        var hospital = await context.Hospitals.FirstOrDefaultAsync(h => h.RegistrationNumber == reportFilter.HospitalRegistrationNumber && !h.IsDeleted);
        if (hospital == null)
            return new Response<HospitalReportDto>(HttpStatusCode.BadRequest, "Hospital not found");

        var startYear = DateTime.SpecifyKind(new DateTime(DateTime.UtcNow.Year, 1, 1), DateTimeKind.Utc);
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
                .Where(p => p.RecordDate >= dateFromUtc && p.RecordDate <= dateToUtc && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            TotalPatientsFromYearStart = await context.Patients
                .Where(p => p.RecordDate >= startYear && p.RecordDate <= now && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            RecoveredInPeriod = await context.Patients
                .Where(p => p.RecordDate >= dateFromUtc && p.RecordDate <= dateToUtc
                        && p.DischargeDate <= dateToUtc && p.DischargeDate >= dateFromUtc
                        && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            RecoveredFromYearStart = await context.Patients
                .Where(p => p.RecordDate >= startYear && p.RecordDate <= now
                        && p.DischargeDate >= startYear && p.DischargeDate <= now
                        && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),

            FluAndColdCount = await context.Patients
                .Where(p => (p.Disease == DiseaseType.Грипп || p.Disease == DiseaseType.ОРЗ)
                            && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            TyphoidCount = await context.Patients
                .Where(p => p.Disease == DiseaseType.БрюшнойТиф
                        && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            HepatitisCount = await context.Patients
                .Where(p => p.Disease == DiseaseType.Гепатит
                        && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),
            OtherDiseasesCount = await context.Patients
                .Where(p => p.Disease != DiseaseType.Грипп
                        && p.Disease != DiseaseType.ОРЗ
                        && p.Disease != DiseaseType.БрюшнойТиф
                        && p.Disease != DiseaseType.Гепатит
                        && p.HospitalRegistrationNumber == hospital.RegistrationNumber)
                .CountAsync(),

            PeriodStart = dateFromUtc,
            PeriodEnd = dateToUtc
        };
        
        return new Response<HospitalReportDto>(hospitalReportDto);
    }
}