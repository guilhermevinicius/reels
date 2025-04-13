using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Genre;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class GetGenreControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task GetGenre_GetGenre_ShouldBeGetGenreSuccessfully()
    {
        // Arrange
        var requestUri = $"/api/v1/genres/2278a870-8dc8-4d70-acb7-f6ece6754b09";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri, null);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }

    [Fact]
    public async Task GetGenre_GetGenre_ShouldBeReturnErrorWhenValidationFails()
    {
        // Arrange
        var requestUri = $"/api/v1/genres/00000000-0000-0000-0000-000000000000";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri, null);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.False(responseDeserialized.Success);
        Assert.Equal("'Id' must not be empty.", responseDeserialized.Messages[0]);
    }
}