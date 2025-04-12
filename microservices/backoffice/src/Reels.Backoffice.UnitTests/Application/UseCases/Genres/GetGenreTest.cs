using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[Collection(nameof(GetGenreAutoMockerCollection))]
public class GetGenreTest(GetGenreAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetGenre_Handler_ShouldBeReturnGenre()
    {
        // Arrange
        var query = new GetGenreQuery(
            Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34"));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetGenre();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetGenre_Handler_ShouldBeReturnErrorWhenGenreNotFound()
    {
        // Arrange
        var query = new GetGenreQuery(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Genre not found", result.Errors[0].Message);
    }
}