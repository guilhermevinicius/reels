using System.Net.Http.Headers;
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
        var file = new ByteArrayContent("Files!"u8.ToArray());
        file.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        var file2 = new ByteArrayContent("Files!"u8.ToArray());
        file2.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

        var form = new MultipartFormDataContent();
        form.Add(new StringContent("Title"), "Title");
        form.Add(new StringContent("Description"), "Description");
        form.Add(new StringContent("2025"), "YearLaunched");
        form.Add(new StringContent("true"), "Opened");
        form.Add(new StringContent("true"), "Published");
        form.Add(new StringContent("90"), "Duration");
        form.Add(new StringContent("1"), "Rating");
        form.Add(file, "Thumb", "image.png");
        form.Add(file2, "ThumbHalf", "image2.png");

        // Action
        var response = await fixture.SendRequest(HttpMethod.Post, requestUri, form);
        var responseDeserialized = await DeserializeResponseHelper.Response(response);

        // Assert
        Assert.True(responseDeserialized.Success);
    }
}