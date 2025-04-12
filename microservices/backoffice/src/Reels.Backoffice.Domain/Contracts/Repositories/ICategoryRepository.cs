using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Category;

namespace Reels.Backoffice.Domain.Contracts.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}