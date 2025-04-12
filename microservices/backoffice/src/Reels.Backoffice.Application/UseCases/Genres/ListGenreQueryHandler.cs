using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.Application.UseCases.Genres;

internal sealed class ListGenreQueryHandler (
    IRepositoryQuery repositoryQuery)
    : IQueryHandler<ListGenreQuery, IEnumerable<GetGenreResponse>>
{
    public async Task<Result<IEnumerable<GetGenreResponse>>> Handle(ListGenreQuery query,
        CancellationToken cancellationToken)
    {
        var categories = repositoryQuery.QueryAsNoTracking<Genre>()
            .Select(category => new GetGenreResponse(
                category.Id,
                category.Name,
                category.IsActive,
                category.CreatedAt))
            .ToArray();

        return await Task.FromResult(categories);
    }
}