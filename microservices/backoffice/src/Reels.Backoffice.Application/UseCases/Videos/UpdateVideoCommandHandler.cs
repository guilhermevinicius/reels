using FluentResults;
using Reels.Backoffice.Application.Contracts.Storage;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.Domain.ValueObjects;

namespace Reels.Backoffice.Application.UseCases.Videos;

internal sealed class UpdateVideoCommandHandler(
    IStorageService storageService,
    IVideoRepository videoRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateVideoCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateVideoCommand command, CancellationToken cancellationToken)
    {
        var video = await videoRepository.GetByIdAsync(command.Id, cancellationToken);
        if (video is null)
            return Result.Fail("Video not found");

        video.Update(
            command.Title,
            command.Description,
            command.YearLaunched,
            command.Opened,
            command.Published,
            command.Duration,
            command.Rating);

        if (command.Thumb is not null)
        {
            var pathThumb = await BuildFile(video.Id, video.Thumb.Path, command.Thumb);
            video.UpdateThumb(pathThumb);
        }

        if (command.ThumbHalf is not null)
        {
            var pathThumbHalf = await BuildFile(video.Id, video.ThumbHalf.Path, command.ThumbHalf);
            video.UpdateThumbHalf(pathThumbHalf);
        }

        return await unitOfWork.CommitAsync(cancellationToken);
    }

    #region Private Methods

    private async Task<string> BuildFile(Guid videoId, string oldPath, MediaMetadata mediaMetadata)
    {
        var path = $"videos/{videoId}/thumbs";
        var extension = mediaMetadata.FileName.Split('.').Last();
        var name = $"{Guid.NewGuid()}.{extension}";

        await storageService.RemoveObject(oldPath);

        return await storageService.UploadFile(
            path,
            mediaMetadata.ContentType,
            name,
            mediaMetadata.File);
    }

    #endregion
}