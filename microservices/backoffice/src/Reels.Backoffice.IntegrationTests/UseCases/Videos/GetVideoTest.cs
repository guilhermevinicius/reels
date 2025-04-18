using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class GetVideoTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task GetVideo_Handler_ShouldBeGetVideoSuccessfully()
    {
        // Arrange
        var command = new GetVideoQuery(
            Guid.Parse("2278a870-8dc8-4d70-acb7-f6ece6754b29"));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsType<GetVideoResponse>(result.Value);
    }

    [Fact]
    public async Task GetVideo_Handler_ShouldBeReturnErrorWhenVideoNotFound()
    {
        // Arrange
        var command = new GetVideoQuery(
            Faker.Random.Guid());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Video not found", result.Errors[0].Message);
    }

    [Fact]
    public async Task GetVideo_Handler_ShouldBeReturnErrorWhenValidatorFails()
    {
        // Arrange
        var command = new GetVideoQuery(
            Guid.Empty);

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Id cannot be empty.", result.Errors[1].Message);
    }
}