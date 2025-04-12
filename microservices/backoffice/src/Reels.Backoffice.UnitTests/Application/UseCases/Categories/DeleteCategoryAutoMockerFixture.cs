using Moq;
using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[CollectionDefinition(nameof(DeleteCategoryAutoMockerCollection))]
public class DeleteCategoryAutoMockerCollection : IClassFixture<DeleteCategoryAutoMockerFixture>;

public class DeleteCategoryAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal DeleteCategoryCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<DeleteCategoryCommandHandler>();
    }

    public void MockGetByIdAsync()
    {
        AutoMocker.GetMock<ICategoryRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetCategory);
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);
    }

    private Category GetCategory()
    {
        return Category.Create(
            Faker.Random.String(),
            Faker.Lorem.Paragraph());
    }

    #endregion
}