using Reels.Backoffice.Api.Extensions;

namespace Reels.Backoffice.Api.Configurations;

internal static class ApiDependencyInjection
{
    internal static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        return services;
    }

    internal static WebApplication UseApi(this WebApplication app)
    {
        app.ApplyMigrations();

        return app;
    }
}