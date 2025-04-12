using Moq;
using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Categories;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Category;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Categories;

[CollectionDefinition(nameof(CreateCategoryAutoMockerCollection))]
public class CreateCategoryAutoMockerCollection : IClassFixture<CreateCategoryAutoMockerFixture>;

public class CreateCategoryAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal CreateCategoryCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<CreateCategoryCommandHandler>();
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);
    }

    #endregion

}