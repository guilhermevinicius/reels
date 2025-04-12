using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[Collection(nameof(CreateGenreAutoMockerCollection))]
public class CreateGenreTest(CreateGenreAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateGenre_Create_ShouldBeCreateSuccessfully()
    {
        // Arrange
        var command = new CreateGenreCommand(
            Faker.Random.String(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task CreateGenre_Create_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new CreateGenreCommand(
            Faker.Random.String(),
            Faker.Random.Bool());

        // Action
        var handler = fixture.GetInstance(false);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Value);
    }
}