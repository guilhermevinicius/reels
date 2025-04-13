namespace Reels.Backoffice.Domain.SeedWorks;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _events = [];
    
    public IReadOnlyCollection<DomainEvent> Events => _events;

    public void RaiseEvent(DomainEvent @event) => _events.Add(@event);

    public void ClearEvents() => _events.Clear();
}