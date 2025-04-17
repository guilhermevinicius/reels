using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Infrastructure.Storage;

namespace Reels.Backoffice.Infrastructure.Configurations;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.ConfigureStorage(configuration);

        return services;
    }

    #region Private Methods

    private static void ConfigureStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageSettings>(options => 
            configuration.GetSection(nameof(StorageSettings)).Bind(options));

        var settings = services.BuildServiceProvider().GetRequiredService<IOptions<StorageSettings>>().Value;

        services.AddMinio(config => config
                .WithEndpoint(settings.Endpoint)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .Build());

        services.AddSingleton<IStorageService, StorageService>();
    }

    #endregion
    
}