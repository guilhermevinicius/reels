using FluentResults;
using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Category;

namespace Reels.Backoffice.Application.UseCases.Categories;

internal sealed class GetCategoryQueryHandler(
    IRepositoryQuery repositoryQuery) 
    : IQueryHandler<GetCategoryQuery, GetCategoryResponse>
{
    public async Task<Result<GetCategoryResponse>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        var category = repositoryQuery.QueryAsNoTracking<Category>()
            .Where(x => x.Id == query.Id)
            .Select(category => new GetCategoryResponse(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt))
            .FirstOrDefault();
        if (category is null)
            return Result.Fail("Category not found");

        return await Task.FromResult(category);
    }
}