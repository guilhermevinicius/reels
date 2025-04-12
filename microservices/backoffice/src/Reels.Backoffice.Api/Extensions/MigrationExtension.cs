using Microsoft.EntityFrameworkCore;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Api.Extensions;

internal static class MigrationExtension
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}