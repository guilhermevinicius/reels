using Reels.Backoffice.Application.SeedWorks.Messaging;

namespace Reels.Backoffice.Application.UseCases.Genres;

public sealed record ListGenreQuery 
    : IQuery<IEnumerable<GetGenreResponse>>;