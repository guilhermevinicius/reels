using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[Collection(nameof(UpdateCategoryAutoMockerCollection))]
public class UpdateCategoryTest(UpdateCategoryAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task UpdateCategory_Handler_ShouldBeUpdatedCorrectly()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            null,
            null);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
    
    [Fact]
    public async Task UpdateCategory_Handler_ShouldBeReturnErrorWhenCategoryNotFound()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            null,
            null);

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Category not found", result.Errors[0].Message);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public async Task UpdateCategory_Handler_ShouldBeActivatedOrDeactivated(bool? isActive)
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            null,
            isActive);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Value);
    }

    [Fact]
    public async Task UpdateCategory_Handler_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            null,
            null);

        // Action
        var handler = fixture.GetInstance(false);
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.Value);
    }
}