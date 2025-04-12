using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description,
    bool? IsActive)
    : ICommand<bool>;