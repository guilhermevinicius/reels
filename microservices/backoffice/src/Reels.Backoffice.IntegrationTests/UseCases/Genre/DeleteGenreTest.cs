using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class DeleteGenreTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeDeletedSuccessfully()
    {
        // Arrange
        var command = new DeleteGenreCommand(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b19"));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeReturnErrorWhenGenreNotFound()
    {
        // Arrange
        var command = new DeleteGenreCommand(
            Faker.Random.Guid());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Genre not found", result.Errors[0].Message);
    }

    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new DeleteGenreCommand(
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