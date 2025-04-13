namespace Reels.Backoffice.Domain.SeedWorks;

public abstract record DomainEvent
{
    public DateTime OccuredOn { get; set; } = DateTime.UtcNow;
}