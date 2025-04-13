using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Domain.Contracts.Repositories;

public interface IVideoRepository : IRepository<Video>
{
    Task<Video?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}