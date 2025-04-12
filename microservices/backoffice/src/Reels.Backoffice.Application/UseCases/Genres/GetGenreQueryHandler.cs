using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.Application.UseCases.Genres;

internal sealed class GetGenreQueryHandler(
    IRepositoryQuery repositoryQuery)
    : IQueryHandler<GetGenreQuery, GetGenreResponse>
{
    public async Task<Result<GetGenreResponse>> Handle(GetGenreQuery query, CancellationToken cancellationToken)
    {
        var category = repositoryQuery.QueryAsNoTracking<Genre>()
            .Where(x => x.Id == query.Id)
            .Select(category => new GetGenreResponse(
                category.Id,
                category.Name,
                category.IsActive,
                category.CreatedAt))
            .FirstOrDefault();
        if (category is null)
            return Result.Fail("Genre not found");

        return await Task.FromResult(category);
    }
}