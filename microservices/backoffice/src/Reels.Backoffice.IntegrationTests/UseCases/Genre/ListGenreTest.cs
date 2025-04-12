using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListGenreTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task ListGenre_Handler_ShouldBeListGenreSuccessfully()
    {
        // Arrange
        var command = new ListGenreQuery();

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsAssignableFrom<IEnumerable<GetGenreResponse>>(result.Value);
    }
}