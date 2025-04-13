namespace Reels.Backoffice.Api.Controllers.V1.Requests;

public sealed record UpdateCategoryRequest(
    string Name,
    string? Description,
    bool? IsActive);