using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Genres;

internal sealed class UpdateGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<UpdateGenreCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
    {
        var genre = await genreRepository.GetByIdAsync(command.Id, cancellationToken);
        if (genre is null)
            return Result.Fail("Genre not found");

        genre.Update(command.Name);

        if (command.IsActive)
            genre.Activate();
        else
            genre.Deactivate();

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}