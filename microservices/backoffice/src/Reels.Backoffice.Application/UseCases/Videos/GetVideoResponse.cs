using Reels.Backoffice.Domain.Models.Video.Enums;

namespace Reels.Backoffice.Application.UseCases.Videos;

public sealed record GetVideoResponse(
    Guid Id,
    string Title,
    string Description,
    int YearLaunched,
    int Duration,
    Rating Rating,
    string? Thumb,
    string? ThumbHalf);