using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListGenreControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task ListGenres_ReturnsListOfGenres()
    {
        // Arrange
        var requestUri = $"/api/v1/genres";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
}