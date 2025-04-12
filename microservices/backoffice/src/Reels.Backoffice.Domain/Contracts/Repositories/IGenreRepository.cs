using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.Domain.Contracts.Repositories;

public interface IGenreRepository : IRepository<Genre> 
{
    Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}