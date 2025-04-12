using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Category;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string description)
    {
        return new Category(name, description);
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description ?? Description;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}