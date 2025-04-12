using Reels.Backoffice.Application.UseCases.Categories;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[Collection(nameof(ListCategoryAutoMockerCollection))]
public class ListCategoryTest(ListCategoryAutoMockerFixture fixture)
{
    [Fact]
    public async Task ListCategory_Handler_ShouldReturnAllCategories()
    {
        // Arrange
        var query = new ListCategoryQuery();
        
        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetAllCategory();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotEmpty(result.Value);
    }
}