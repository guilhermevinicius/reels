using Moq;
using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.Domain.Contracts.Repositories;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.Domain.SeedWorks;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[CollectionDefinition(nameof(DeleteGenreAutoMockerCollection))]
public class DeleteGenreAutoMockerCollection : IClassFixture<DeleteGenreAutoMockerFixture>;

public class DeleteGenreAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal DeleteGenreCommandHandler GetInstance(bool isSuccess = true)
    {
        AutoMocker = new AutoMocker();
        MockCommit(isSuccess);
        return AutoMocker.CreateInstance<DeleteGenreCommandHandler>();
    }

    public void MockGetByIdAsync()
    {
        AutoMocker.GetMock<IGenreRepository>()
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(GetGenre);
    }

    #region Private Methods

    private void MockCommit(bool isSuccess = true)
    {
        AutoMocker.GetMock<IUnitOfWork>()
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSuccess);
    }

    private Genre GetGenre()
    {
        return Genre.Create(
            Faker.Random.String(),
            Faker.Random.Bool());
    }

    #endregion
}