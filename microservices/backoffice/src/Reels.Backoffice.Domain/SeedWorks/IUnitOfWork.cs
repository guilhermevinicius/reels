namespace Reels.Backoffice.Domain.SeedWorks;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken);
}