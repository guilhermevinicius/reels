using Microsoft.EntityFrameworkCore;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Video;
using Reels.Backoffice.Persistence.Configurations;

namespace Reels.Backoffice.Persistence.Repository;

internal sealed class VideoRepository(DataContext context) : IVideoRepository
{
    private readonly DbSet<Video> _videos = context.Set<Video>();

    public async Task InsertAsync(Video entity, CancellationToken cancellationToken)
    {
        await _videos.AddAsync(entity, cancellationToken);
    }

    public void DeleteAsync(Video entity)
    {
        _videos.Remove(entity);
    }

    public async Task<Video?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _videos.FirstOrDefaultAsync(x =>
            x.Id == id, cancellationToken);
    }
}