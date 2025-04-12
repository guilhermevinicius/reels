using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Persistence.Repository;

internal sealed class UnitOfWork(DataContext context) : IUnitOfWork
{
    public async Task<bool> CommitAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}