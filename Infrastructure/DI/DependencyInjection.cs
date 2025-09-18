using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<HospitalService>();
        services.AddScoped<PatientService>();
        services.AddScoped<ReportService>();
    }
}