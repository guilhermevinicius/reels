using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[Collection(nameof(CreateCategoryAutoMockerCollection))]
public class CreateCategoryTest(CreateCategoryAutoMockerFixture fixture) : BaseTest
{
    [Fact]
    public async Task CreateCategory_Create_ShouldBeCreateSuccessfully()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Faker.Random.String(),
            Faker.Random.String());

        // Action
        var handler = fixture.GetInstance();
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task CreateCategory_Create_ShouldBeReturnErrorWhenCommitFails()
    {
        // Arrange
        var command = new CreateCategoryCommand(
            Faker.Random.String(),
            Faker.Random.String());

        // Action
        var handler = fixture.GetInstance(false);
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Value);
    }
}