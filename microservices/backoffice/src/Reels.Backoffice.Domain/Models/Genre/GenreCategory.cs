using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Genre;

public class GenreCategory : Entity
{
    public Genre Genre { get; private set; }
    public Guid GenreId { get; private set; }
    public Guid CategoryId { get; private set; }

    private GenreCategory(Guid genreId, Guid categoryId)
    {
        GenreId = genreId;
        CategoryId = categoryId;
    }

    public static GenreCategory Create(Guid genreId, Guid categoryId)
    {
        return new GenreCategory(genreId, categoryId);
    }
}