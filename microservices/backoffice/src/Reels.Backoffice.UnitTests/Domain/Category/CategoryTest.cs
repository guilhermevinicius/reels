using Reels.Backoffice.UnitTests.Common;
using Entity = Reels.Backoffice.Domain.Models.Category;

namespace Reels.Backoffice.UnitTests.Domain.Category;

public class CategoryTest : BaseTest
{
    [Fact]
    public void Category_Create_ShouldBeCreatedCorrectly()
    {
        // Arrange
        var name = Faker.Lorem.Text();
        var description = Faker.Lorem.Text();

        // Action
        var category = Entity.Category.Create(name, description);

        // Assert
        Assert.Equal(name, category.Name);
        Assert.Equal(description, category.Description);
        Assert.True(category.IsActive);
        Assert.NotEqual(DateTime.Now, category.CreatedAt);
    }

    [Fact]
    public void Category_Update_ShouldBeUpdated()
    {
        // Arrange
        var name = Faker.Random.String();
        var description = Faker.Random.String();
        var category = GetCategory();

        // Action
        category.Update(name, description);

        // Assert
        Assert.Equal(name, category.Name);
        Assert.Equal(description, category.Description);
    }

    [Fact]
    public void Category_Activate_ShouldBeActivatedCategory()
    {
        // Arrange
        var category = GetCategory();

        // Action
        category.Deactivate();
        category.Activate();

        // Assert
        Assert.True(category.IsActive);
    }
    
    [Fact]
    public void Category_Activate_ShouldBeDeactivatedCategory()
    {
        // Arrange
        var category = GetCategory();

        // Action
        category.Deactivate();

        // Assert
        Assert.False(category.IsActive);
    }
    
    #region Private Methods

    private Entity.Category GetCategory()
    {
        return Entity.Category.Create(
            Faker.Random.String(),
            Faker.Random.String());
    }

    #endregion
}