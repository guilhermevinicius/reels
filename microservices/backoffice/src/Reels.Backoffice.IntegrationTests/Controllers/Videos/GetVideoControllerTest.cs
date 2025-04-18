using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class GetVideoControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task GetVideo_ReturnsVideo()
    {
        // Arrange
        var requestUri = $"/api/v1/videos/2278a870-8dc8-4d70-acb7-f6ece6754b29";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
    
    [Fact]
    public async Task GetVideo_ShouldBeReturnErrorWhenVideoNotFound()
    {
        // Arrange
        var requestUri = $"/api/v1/videos/2278a870-8dc8-4d70-acb7-f7ece6754b29";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.False(responseDeserialized.Success);
    }
}