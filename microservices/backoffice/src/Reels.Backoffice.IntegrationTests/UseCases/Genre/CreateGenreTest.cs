using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateGenreTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateGenre_Handler_ShouldBeCreatedSuccessfully()
    {
        // Arrange
        var command = new CreateGenreCommand(
            Faker.Lorem.Text(),
            Faker.Random.Bool());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task CreateGenre_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new CreateGenreCommand(
            null,
            Faker.Random.Bool());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Name is required.", result.Errors[1].Message);
    }
}