using Reels.Backoffice.UnitTests.Common;
using Entity = Reels.Backoffice.Domain.Models.Genre;

namespace Reels.Backoffice.UnitTests.Domain.Genre;

public class GenreTest : BaseTest
{
    [Fact]
    public void Genre_Create_ShouldBeCreatedCorrectly()
    {
        // Arrange
        var name = Faker.Lorem.Text();
        var isActive = Faker.Random.Bool();

        // Action
        var genre = Entity.Genre.Create(name, isActive);

        // Assert
        Assert.Equal(name, genre.Name);
        Assert.Equal(isActive, genre.IsActive);
        Assert.NotEqual(DateTime.Now, genre.CreatedAt);
    }

    [Fact]
    public void Genre_Update_ShouldBeUpdated()
    {
        // Arrange
        var name = Faker.Random.String();
        var genre = GetGenre();

        // Action
        genre.Update(name);

        // Assert
        Assert.Equal(name, genre.Name);
    }

    [Fact]
    public void Genre_Activate_ShouldBeActivatedGenre()
    {
        // Arrange
        var genre = GetGenre();

        // Action
        genre.Deactivate();
        genre.Activate();

        // Assert
        Assert.True(genre.IsActive);
    }
    
    [Fact]
    public void Genre_Activate_ShouldBeDeactivatedGenre()
    {
        // Arrange
        var genre = GetGenre();

        // Action
        genre.Deactivate();

        // Assert
        Assert.False(genre.IsActive);
    }
    
    [Fact]
    public void Genre_AddCategory_ShouldBeAddCategory()
    {
        // Arrange
        var genre = GetGenre();

        // Action
        genre.AddCategory(Guid.NewGuid());

        // Assert
        Assert.Single(genre.GenreCategories);
    }
    
    [Fact]
    public void Genre_RemoveCategory_ShouldBeRemoveCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var genre = GetGenre();
        genre.AddCategory(categoryId);
        genre.AddCategory(Guid.NewGuid());

        // Action
        genre.RemoveCategory(categoryId);

        // Assert
        Assert.Single(genre.GenreCategories);
    }
    
    [Fact]
    public void Genre_RemoveAllCategory_ShouldBeRemoveAllCategories()
    {
        // Arrange
        var genre = GetGenre();
        genre.AddCategory(Guid.NewGuid());

        // Action
        genre.RemoveAllCategories();

        // Assert
        Assert.Empty(genre.GenreCategories);
    }

    #region Private Methods

    private static Entity.Genre GetGenre()
    {
        return Entity.Genre.Create(
            "Genre");
    }

    #endregion
    
}