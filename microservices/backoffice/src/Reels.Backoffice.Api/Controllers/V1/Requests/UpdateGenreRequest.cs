namespace Reels.Backoffice.Api.Controllers.V1.Requests;

public record UpdateGenreRequest(
    string Name,
    bool IsActive);