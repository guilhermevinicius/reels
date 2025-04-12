using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.Persistence.Repository;
using Reels.Backoffice.Persistence.Repository.Generics;

namespace Reels.Backoffice.Persistence.Configurations;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection ConfigurePersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextPool<DataContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("Postgres"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepositoryQuery, RepositoryQuery>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}