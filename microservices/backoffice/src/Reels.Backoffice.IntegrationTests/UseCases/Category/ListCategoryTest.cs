using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.IntegrationTests.Config;

namespace Reels.Backoffice.IntegrationTests.UseCases.Category;

[Collection(nameof(InfraIntegrationTestsCollection))]
public class ListCategoryTest(InfraIntegrationTestsFixture fixture) : BaseTest
{
    [Fact]
    public async Task ListCategory_Handler_ShouldBeListCategorySuccessfully()
    {
        // Arrange
        var command = new ListCategoryQuery();

        // Action
        var result = await fixture.Sender.Send(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.IsAssignableFrom<IEnumerable<GetCategoryResponse>>(result.Value);
    }
}