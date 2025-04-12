using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record CreateCategoryCommand(
    string Name,
    string Description)
    : ICommand<bool>;