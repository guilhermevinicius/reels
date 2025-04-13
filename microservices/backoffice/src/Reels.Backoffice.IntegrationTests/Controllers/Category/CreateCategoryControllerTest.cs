using Reels.Backoffice.IntegrationTests.Common;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.Controllers.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateCategoryControllerTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task CreateCategory_Create_ShouldBeCreatedSuccessfully()
    {
        // Arrange
        var requestUri = $"/api/v1/categories";
        var json = $@"{{
            ""name"": ""Category"",
            ""description"": ""Category""
        }}";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Post, requestUri, json);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
    
    [Fact]
    public async Task CreateCategory_Create_ShouldBeReturnErrorWhenValidationFails()
    {
        // Arrange
        var requestUri = $"/api/v1/categories";
        var json = $@"{{
            ""name"": """",
            ""description"": ""Category""
        }}";

        // Action
        var response = await fixture.SendRequest(HttpMethod.Post, requestUri, json);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.False(responseDeserialized.Success);
        Assert.Equal("Name is required.", responseDeserialized.Messages[0]);
    }
}