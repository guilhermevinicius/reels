using Microsoft.EntityFrameworkCore;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Persistence.Repository;

internal sealed class GenreRepository(DataContext context) : IGenreRepository
{
    private readonly DbSet<Genre> _genres = context.Set<Genre>();

    public async Task InsertAsync(Genre entity, CancellationToken cancellationToken)
    {
        await _genres.AddAsync(entity, cancellationToken);
    }

    public void DeleteAsync(Genre entity)
    {
        _genres.Remove(entity);
    }

    public async Task<Genre?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _genres.FirstOrDefaultAsync(x =>
            x.Id == id, cancellationToken);
    }
}