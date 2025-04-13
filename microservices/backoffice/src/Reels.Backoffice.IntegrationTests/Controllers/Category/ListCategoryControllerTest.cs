using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListCategoryControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task ListCategories_ReturnsListOfCategories()
    {
        // Arrange
        var requestUri = $"/api/v1/categories";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Get, requestUri, null);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
}