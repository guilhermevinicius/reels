using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record GetCategoryQuery(
    Guid Id)
    : IQuery<GetCategoryResponse>;