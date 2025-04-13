namespace Reels.Backoffice.Api.DTOs;

public record CustomResponse(
    bool Success,
    int StatusCode,
    object Data,
    List<string> Messages,
    DateTimeOffset DateTimeOffset);