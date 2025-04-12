using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Category;

namespace Reels.Backoffice.Application.UseCases.Categories;

internal sealed class ListCategoryQueryHandler (
    IRepositoryQuery repositoryQuery)
    : IQueryHandler<ListCategoryQuery, IEnumerable<GetCategoryResponse>>
{
    public async Task<Result<IEnumerable<GetCategoryResponse>>> Handle(ListCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var categories = repositoryQuery.QueryAsNoTracking<Category>()
            .Select(category => new GetCategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt))
            .ToArray();

        return await Task.FromResult(categories);
    }
}