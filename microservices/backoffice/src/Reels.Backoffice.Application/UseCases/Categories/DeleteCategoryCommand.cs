using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Categories;

public sealed record DeleteCategoryCommand(
    Guid Id)
    : ICommand<bool>;