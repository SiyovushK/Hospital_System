using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Extensions;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v1",
                Description = "API Description",
                Contact = new OpenApiContact
                {
                    Name = "Hospital system"
                }
            });
        });
    }
}