using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateCategoryTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateCategory_Handler_ShouldBeCreatedSuccessfully()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Faker.Lorem.Text(),
            Faker.Lorem.Text());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
    
    [Fact]
    public async Task CreateCategory_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Faker.Lorem.Text(),
            null);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Description is required.", result.Errors[1].Message);
    }
}