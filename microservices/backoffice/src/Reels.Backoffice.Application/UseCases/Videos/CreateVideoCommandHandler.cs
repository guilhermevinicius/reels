using FluentResults;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Video;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.Domain.ValueObjects;

namespace Reels.Backoffice.Application.UseCases.Videos;

internal sealed class CreateVideoCommandHandler(
    IStorageService storageService,
    IVideoRepository videoRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateVideoCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateVideoCommand command, CancellationToken cancellationToken)
    {
        var video = Video.Create(
            command.Title,
            command.Description,
            command.YearLaunched,
            command.Opened,
            command.Published,
            command.Duration,
            command.Rating);

        await videoRepository.InsertAsync(video, cancellationToken);

        var pathThumb = await BuildFile(video.Id, command.Thumb);

        video.UpdateThumb(pathThumb);

        var pathThumbHalf = await BuildFile(video.Id, command.ThumbHalf);

        video.UpdateThumbHalf(pathThumbHalf);

        return await unitOfWork.CommitAsync(cancellationToken);
    }

    #region Private Methods

    private async Task<string> BuildFile(Guid videoId, MediaMetadata mediaMetadata)
    {
        var path = $"videos/{videoId}/thumbs";
        var extension = mediaMetadata.FileName.Split('.').Last();
        var name = $"{Guid.NewGuid()}.{extension}";

        return await storageService.UploadFile(
            path,
            mediaMetadata.ContentType,
            name,
            mediaMetadata.File);
    }

    #endregion
}