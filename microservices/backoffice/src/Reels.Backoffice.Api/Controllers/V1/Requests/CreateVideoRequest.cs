using Reels.Backoffice.Domain.Models.Video.Enums;

namespace Reels.Backoffice.Api.Controllers.V1.Requests;

public sealed class CreateVideoRequest
{
    public string Title { get; init; }
    public string Description { get; init; }
    public int YearLaunched { get; init; }
    public bool Opened { get; init; }
    public bool Published { get; init; }
    public int Duration { get; init; }
    public Rating Rating { get; init; }
}