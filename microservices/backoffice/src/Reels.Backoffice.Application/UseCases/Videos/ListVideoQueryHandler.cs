using FluentResults;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Application.UseCases.Videos;

internal sealed class ListVideoQueryHandler(
    IRepositoryQuery repositoryQuery,
    IStorageService storageService) 
    : IQueryHandler<ListVideoQuery, IEnumerable<GetVideoResponse>>
{
    public async Task<Result<IEnumerable<GetVideoResponse>>> Handle(ListVideoQuery request, CancellationToken cancellationToken)
    {
        var videos = repositoryQuery.QueryAsNoTracking<Video>()
            .Select(video => new GetVideoResponse(
                video.Id,
                video.Title,
                video.Description,
                video.YearLaunched,
                video.Duration,
                video.Published,
                video.Opened,
                video.Rating,
                 storageService.GetObjectUrl(video.Thumb.Path).GetAwaiter().GetResult(),
                storageService.GetObjectUrl(video.ThumbHalf.Path).GetAwaiter().GetResult(),
                video.CreatedAt))
            .ToArray();

        return await Task.FromResult(videos);
    }
}