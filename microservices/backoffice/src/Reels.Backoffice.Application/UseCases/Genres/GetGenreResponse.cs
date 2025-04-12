namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed record GetGenreResponse(
    Guid Id,
    string Name,
    bool IsActive,
    DateTime CreatedAt);