using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class UpdateGenreTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task UpdateGenre_Handler_ShouldBeUpdatedSuccessfully()
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b09"),
            Faker.Lorem.Text(),
            Faker.Random.Bool());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task UpdateGenre_Handler_ShouldBeReturnErrorWhenGenreNotFound()
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Faker.Random.Guid(),
            Faker.Lorem.Text(),
            Faker.Random.Bool());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Genre not found", result.Errors[0].Message);
    }

    [Fact]
    public async Task UpdateGenre_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Faker.Random.Guid(),
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