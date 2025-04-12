using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[Collection(nameof(GetCategoryAutoMockerCollection))]
public class GetCategoryTest(GetCategoryAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetCategory_Handler_ShouldBeReturnCategory()
    {
        // Arrange
        var query = new GetCategoryQuery(
            Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34"));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetCategory();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetCategory_Handler_ShouldBeReturnErrorWhenCategoryNotFound()
    {
        // Arrange
        var query = new GetCategoryQuery(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}