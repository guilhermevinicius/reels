using Reels.Backoffice.Application.SeedWorks.Messaging;
using Reels.Backoffice.Domain.Models.Video.Enums;

namespace Reels.Backoffice.Application.UseCases.Videos;

public sealed record CreateVideoCommand(
    string Title,
    string Description,
    int YearLaunched,
    bool Opened,
    bool Published,
    int Duration,
    Rating Rating)
    : ICommand<bool>;