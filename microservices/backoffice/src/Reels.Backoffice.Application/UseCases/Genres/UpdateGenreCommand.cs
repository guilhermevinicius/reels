using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed record UpdateGenreCommand(
    Guid Id,
    string Name,
    bool IsActive)
    : ICommand<bool>;