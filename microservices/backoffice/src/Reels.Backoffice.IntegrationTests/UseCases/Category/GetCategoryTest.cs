using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class GetCategoryTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetCategory_Handler_ShouldBeGetCategorySuccessfully()
    {
        // Arrange
        var command = new GetCategoryQuery(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b09"));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value.Name);
        Assert.NotNull(result.Value.Description);
        Assert.IsType<GetCategoryResponse>(result.Value);
    }

    [Fact]
    public async Task GetCategory_Handler_ShouldBeReturnErrorWhenCategoryNotFound()
    {
        // Arrange
        var command = new GetCategoryQuery(
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
    public async Task GetCategory_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new GetCategoryQuery(
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