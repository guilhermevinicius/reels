using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Videos;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class CreateVideoTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateVideo_WithValidData_ShouldSucceed()
    {
        // Arrange
        var command = new CreateVideoCommand(
            Faker.Random.String(10),
            Faker.Random.String(10),
            Faker.Random.Int(),
            Faker.Random.Bool(),
            Faker.Random.Bool(),
            Faker.Random.Int(),
            Faker.Random.Enum<Rating>());

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }
}