using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed record DeleteGenreCommand(
    Guid Id)
    : ICommand<bool>;