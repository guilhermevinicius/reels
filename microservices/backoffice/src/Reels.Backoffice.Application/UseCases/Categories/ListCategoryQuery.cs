using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record ListCategoryQuery 
    : IQuery<IEnumerable<GetCategoryResponse>>;