using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class UpdateCategoryTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task UpdateCategory_Handler_ShouldBeUpdatedSuccessfully()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b09"),
            Faker.Lorem.Text(),
            null,
            null);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

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
            Faker.Lorem.Text(),
            null,
            null);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Category not found", result.Errors[0].Message);
    }
    
    [Fact]
    public async Task UpdateCategory_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            Faker.Random.Guid(),
            null,
            null,
            null);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Name is required.", result.Errors[1].Message);
    }
}