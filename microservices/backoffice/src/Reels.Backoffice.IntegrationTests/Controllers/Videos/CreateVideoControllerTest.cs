using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateVideoControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task Video_CreateVideo_ShouldBeCreatedCorrectly()
    {
        // Arrange
        var requestUri = $"/api/v1/videos";
        var json = $@"{{
            ""title"": ""Title"",
            ""description"": ""Description"",
            ""yearLaunched"": ""2024"",
            ""opened"": true,
            ""published"": true,
            ""duration"": 3600,
            ""rating"": 3
        }}";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Post, requestUri, json);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
}