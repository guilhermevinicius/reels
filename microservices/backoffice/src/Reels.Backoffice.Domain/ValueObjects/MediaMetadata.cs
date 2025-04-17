namespace Reels.Backoffice.Domain.ValueObjects;

public sealed record MediaMetadata(
    string FileName,
    string ContentType,
    Stream File);