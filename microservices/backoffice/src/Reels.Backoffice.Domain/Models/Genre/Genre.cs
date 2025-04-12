using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Genre;

public class Genre : AggregateRoot
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    private List<GenreCategory> _genreCategories = [];
    public IReadOnlyCollection<GenreCategory> GenreCategories => _genreCategories;

    private Genre(string name, bool isActive = true)
    {
        Name = name;
        IsActive = isActive;
    }

    public static Genre Create(string name, bool isActive = true)
    {
        return new Genre(name, isActive);
    }

    public void Update(string name)
    {
        Name = name;
    }
    
    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void AddCategory(Guid categoryId)
    {
        _genreCategories.Add(GenreCategory.Create(Id, categoryId));
    }

    public void RemoveCategory(Guid categoryId)
    {
        _genreCategories = _genreCategories.Where(x => 
            x.GenreId == Id
            && x.CategoryId != categoryId).ToList();
    }

    public void RemoveAllCategories()
    {
        _genreCategories.Clear();
    }
}