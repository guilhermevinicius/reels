using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed record CreateGenreCommand(
    string Name,
    bool IsActive)
    : ICommand<bool>;