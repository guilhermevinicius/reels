using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Video.Events;

public sealed record VideoUploadedEvent(
    Guid ResourceId,
    string FilePath)
    : DomainEvent;