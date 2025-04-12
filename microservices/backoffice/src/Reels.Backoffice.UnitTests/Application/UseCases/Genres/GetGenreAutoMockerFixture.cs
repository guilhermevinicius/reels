using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Genres;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Genre;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Genres;

[CollectionDefinition(nameof(GetGenreAutoMockerCollection))]
public class GetGenreAutoMockerCollection : IClassFixture<GetGenreAutoMockerFixture>;

public class GetGenreAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new ();

    internal GetGenreQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<GetGenreQueryHandler>();
    }

    public void MockGetGenre()
    {
        AutoMocker.GetMock<IRepositoryQuery>()
            .Setup(x => x.QueryAsNoTracking<Genre>())
            .Returns(new List<Genre>()
            {
                GetGenre()
            }.AsQueryable());
    }

    #region Private Methods

    private Genre GetGenre()
    {
        var category = Genre.Create(
            Faker.Random.String(),
            Faker.Random.Bool());

        category.Id = Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34");
        
        return category;
    }

    #endregion
    
}