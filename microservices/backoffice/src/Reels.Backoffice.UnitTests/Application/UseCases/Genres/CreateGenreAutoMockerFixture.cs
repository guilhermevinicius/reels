using Moq;
using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[CollectionDefinition(nameof(CreateGenreAutoMockerCollection))]
public class CreateGenreAutoMockerCollection : IClassFixture<CreateGenreAutoMockerFixture>;

public class CreateGenreAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal CreateGenreCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<CreateGenreCommandHandler>();
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