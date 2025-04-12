using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[CollectionDefinition(nameof(ListGenreAutoMockerCollection))]
public class ListGenreAutoMockerCollection : IClassFixture<ListGenreAutoMockerFixture>;

public class ListGenreAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal ListGenreQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<ListGenreQueryHandler>();
    }

    public void MockGetAllGenre()
    {
        AutoMocker.GetMock<IRepositoryQuery>()
            .Setup(x => x.QueryAsNoTracking<Genre>())
            .Returns(new List<Genre>()
            {
                GetGenre(),
                GetGenre()
            }.AsQueryable());
    }

    #region Private Methods

    private Genre GetGenre()
    {
        var category = Genre.Create(
            Faker.Random.String(),
            Faker.Random.Bool());

        return category;
    }

    #endregion

}