using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[Collection(nameof(DeleteGenreAutoMockerCollection))]
public class DeleteGenreTest(DeleteGenreAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeDeletedCorrectly()
    {
        // Arrange
        var command = new DeleteGenreCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeReturnErrorWhenGenreNotFound()
    {
        // Arrange
        var command = new DeleteGenreCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeleteGenre_Handler_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new DeleteGenreCommand(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance(false);
        fixture.MockGetByIdAsync();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Value);
    }
}