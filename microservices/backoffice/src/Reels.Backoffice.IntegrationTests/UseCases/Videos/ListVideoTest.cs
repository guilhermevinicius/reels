using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListVideoTest(InfraIntegrationTestsFixture fixture)
{
    [Fact]
    public async Task ListVideo_Handler_ShouldBeListVideoSuccessfully()
    {
        // Arrange
        var query = new ListVideoQuery();

        // Action
        var result = await fixture.Sender.Send(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsAssignableFrom<IEnumerable<GetVideoResponse>>(result.Value);
    }
}