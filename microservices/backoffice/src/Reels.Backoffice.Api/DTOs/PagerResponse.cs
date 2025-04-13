namespace Reels.Backoffice.Api.DTOs;

public sealed record PagerResponse<T>(
    int PageNumber,
    int TotalRows,
    IEnumerable<T> Items);
