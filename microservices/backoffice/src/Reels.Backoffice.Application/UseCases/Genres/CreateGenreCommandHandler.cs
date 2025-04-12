using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Genres;

internal sealed class CreateGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGenreCommand, bool>
{
    public async Task<Result<bool>> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var category = Genre.Create(
            command.Name,
            command.IsActive);

        await genreRepository.InsertAsync(category, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}