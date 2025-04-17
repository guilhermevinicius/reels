using System.Text;
using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.Domain.ValueObjects;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateVideoTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateVideo_WithValidData_ShouldSucceed()
    {
        // Arrange
        var file = new MemoryStream("Files!"u8.ToArray());
        var file2 = new MemoryStream("Files this is text to files!"u8.ToArray());

        var command = new CreateVideoCommand(
            Faker.Lorem.Text(),
            Faker.Lorem.Text(),
            Faker.Lorem.Random.Int(),
            Faker.Random.Bool(),
            Faker.Random.Bool(),
            Faker.Random.Int(),
            Faker.Random.Enum<Rating>(),
            new MediaMetadata(
                $"{Faker.Lorem.Text()[..10]}.png",
                "image/png",
                file),
            new MediaMetadata(
                $"{Faker.Lorem.Text()[..10]}.png",
                "image/png",
                file2));

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}