using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Videos;

[Collection(nameof(GetVideoAutoMockerCollection))]
public class GetVideoTest(GetVideoAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetVideo_Handler_ShouldBeReturnVideo()
    {
        // Arrange
        var query = new GetVideoQuery(
            Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34"));

        // Action
        var handler = fixture.GetInstance();
        fixture.MockGetCategory();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetVideo_Handler_ShouldBeReturnErrorWhenVideoNotFound()
    {
        // Arrange
        var query = new GetVideoQuery(
            Faker.Random.Guid());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailed);
        Assert.Equal("Video not found", result.Errors[0].Message);
    }
}