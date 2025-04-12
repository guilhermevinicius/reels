using Reels.Backoffice.Application.UseCases.Genres;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[Collection(nameof(ListGenreAutoMockerCollection))]
public class ListGenreTest(ListGenreAutoMockerFixture fixture)
{
    [Fact]
    public async Task ListGenre_Handler_ShouldReturnAllGenres()
    {
        // Arrange
        var query = new ListGenreQuery();
        
        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetAllGenre();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotEmpty(result.Value);
    }
}