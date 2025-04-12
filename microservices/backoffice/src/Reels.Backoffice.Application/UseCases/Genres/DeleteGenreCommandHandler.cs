using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Application.UseCases.Genres;

internal sealed class DeleteGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<DeleteGenreCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
    {
        var category = await genreRepository.GetByIdAsync(command.Id, cancellationToken);
        if (category is null)
            return Result.Fail("Genre not found");

        genreRepository.DeleteAsync(category);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}