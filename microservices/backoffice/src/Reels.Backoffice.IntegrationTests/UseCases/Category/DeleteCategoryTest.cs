using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class DeleteCategoryTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeDeletedSuccessfully()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b19"));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeReturnErrorWhenCategoryNotFound()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Faker.Random.Guid());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Category not found", result.Errors[0].Message);
    }
    
    [Fact]
    public async Task DeleteCategory_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new DeleteCategoryCommand(
            Guid.Empty);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Id cannot be empty.", result.Errors[1].Message);
    }
}