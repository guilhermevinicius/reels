using Reels.Backoffice.Api.Extensions;
using Scalar.AspNetCore;

namespace Reels.Backoffice.Api.Configurations;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        services.AddCors();

        services.AddOpenApi();

        services.AddProblemDetails();

        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddControllers();

        return services;
    }

    internal static WebApplication UseApi(this WebApplication app)
    {
        app.MapOpenApi();

        app.ApplyMigrations();

        app.MapScalarApiReference(options =>
        {
            var prefix = app.Environment.IsDevelopment() 
                ? "/"
                : "/backoffice"; 

            options.Servers = new List<ScalarServer>()
            {
                new(prefix)
            };
        });

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}