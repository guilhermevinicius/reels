using Microsoft.EntityFrameworkCore;

namespace Reels.Backoffice.Persistence.Configurations;

public class DataContext(DbContextOptions<DataContext> options) 
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}