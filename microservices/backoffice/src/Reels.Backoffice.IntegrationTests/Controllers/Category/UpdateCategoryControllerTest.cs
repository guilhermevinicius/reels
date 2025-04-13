using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class UpdateCategoryControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task UpdateCategory_UpdateCategory_ShouldBeUpdatedSuccessfully()
    {
        // Arrange
        var requestUri = $"/api/v1/categories/2278a870-8dc8-4d70-acb7-f6ece6754b09";
        var json = $@"{{
            ""name"": ""Category 1"",
            ""description"": ""Category 1"",
            ""isActive"": true
        }}";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Put, requestUri, json);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }

    [Fact]
    public async Task UpdateCategory_UpdateCategory_ShouldBeReturnErrorWhenValidationFails()
    {
        // Arrange
        var requestUri = $"/api/v1/categories/2278a870-8dc8-4d70-acb7-f6ece6754b09";
        var json = $@"{{
            ""name"": """",
            ""description"": ""Category 1"",
            ""isActive"": true
        }}";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Put, requestUri, json);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.False(responseDeserialized.Success);
        Assert.Equal("'Name' must not be empty.", responseDeserialized.Messages[0]);
    }
}