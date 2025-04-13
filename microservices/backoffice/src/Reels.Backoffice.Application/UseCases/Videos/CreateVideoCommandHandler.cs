using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Video;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Videos;

internal sealed class CreateVideoCommandHandler(
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
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}