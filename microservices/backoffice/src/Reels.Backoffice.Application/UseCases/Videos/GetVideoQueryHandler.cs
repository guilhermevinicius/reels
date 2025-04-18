using FluentResults;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.Application.UseCases.Videos;

internal sealed class GetVideoQueryHandler(
    IRepositoryQuery repositoryQuery,
    IStorageService storageService)
    : IQueryHandler<GetVideoQuery, GetVideoResponse>
{
    public async Task<Result<GetVideoResponse>> Handle(GetVideoQuery request, CancellationToken cancellationToken)
    {
        var video = repositoryQuery.QueryAsNoTracking<Video>()
            .Where(video => video.Id == request.Id)
            .Select(video => new GetVideoResponse(
                video.Id,
                video.Title,
                video.Description,
                video.YearLaunched,
                video.Duration,
                video.Rating,
                storageService.GetObjectUrl(video.Thumb.Path).GetAwaiter().GetResult(),
                storageService.GetObjectUrl(video.ThumbHalf.Path).GetAwaiter().GetResult()))
            .FirstOrDefault();

        if (video is null)
            return Result.Fail("Video not found");

        return await Task.FromResult(video);
    }
}