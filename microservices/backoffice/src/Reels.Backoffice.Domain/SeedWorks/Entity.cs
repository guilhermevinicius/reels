namespace Reels.Backoffice.Domain.SeedWorks;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}