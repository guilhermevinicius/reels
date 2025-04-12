namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record GetCategoryResponse(
    Guid Id,
    string Name,
    string Description,
    bool IsActive,
    DateTime CreatedAt);