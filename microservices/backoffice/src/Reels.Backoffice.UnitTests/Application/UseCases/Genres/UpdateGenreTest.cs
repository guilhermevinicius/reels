using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[Collection(nameof(UpdateGenreAutoMockerCollection))]
public class UpdateGenreTest(UpdateGenreAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task UpdateGenre_Handler_ShouldBeUpdatedCorrectly()
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

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
            Faker.Random.String(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Genre not found", result.Errors[0].Message);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UpdateGenre_Handler_ShouldBeActivatedOrDeactivated(bool isActive)
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            isActive);

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.Value);
    }

    [Fact]
    public async Task UpdateGenre_Handler_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new UpdateGenreCommand(
            Faker.Random.Guid(),
            Faker.Random.String(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance(false);
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.Value);
    }
}