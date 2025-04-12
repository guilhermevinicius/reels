using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[Collection(nameof(DeleteCategoryAutoMockerCollection))]
public class DeleteCategoryTest(DeleteCategoryAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeDeletedCorrectly()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeReturnErrorWhenCategoryNotFound()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance(false);
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Value);
    }
}