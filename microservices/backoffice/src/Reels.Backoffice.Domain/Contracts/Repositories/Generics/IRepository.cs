using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Contracts.Repositories.Generics;

public interface IRepository<in T> where T : Entity
{
    Task InsertAsync(T entity, CancellationToken cancellationToken);

    void DeleteAsync(T entity);
}