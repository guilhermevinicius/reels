using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListVideoControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task ListVideo_ReturnsListOfVideos()
    {
        // Arrange
        var requestUri = $"/api/v1/videos";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
}