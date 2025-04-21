using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.Domain.ValueObjects;

namespace Reels.Backoffice.Application.UseCases.Videos;

public sealed record UpdateVideoCommand(
    Guid Id,
    string Title,
    string Description,
    int YearLaunched,
    bool Opened,
    bool Published,
    int Duration,
    Rating Rating,
    MediaMetadata? Thumb,
    MediaMetadata? ThumbHalf)
    : ICommand<bool>;