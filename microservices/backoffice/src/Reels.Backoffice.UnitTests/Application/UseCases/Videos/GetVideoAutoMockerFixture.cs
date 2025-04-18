using Moq.AutoMock;
using Reels.Backoffice.Application.UseCases.Videos;
using Reels.Backoffice.Domain.Contracts.Repositories.Generics;
using Reels.Backoffice.Domain.Models.Video;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.UnitTests.Common;

namespace Reels.Backoffice.UnitTests.Application.UseCases.Videos;

[CollectionDefinition(nameof(GetVideoAutoMockerCollection))]
public class GetVideoAutoMockerCollection : IClassFixture<GetVideoAutoMockerFixture>;

public class GetVideoAutoMockerFixture : BaseTest
{
    public AutoMocker AutoMocker = new();

    internal GetVideoQueryHandler GetInstance()
    {
        AutoMocker = new AutoMocker();
        return AutoMocker.CreateInstance<GetVideoQueryHandler>();
    }

    public void MockGetCategory()
    {
        AutoMocker.GetMock<IRepositoryQuery>()
            .Setup(x => x.QueryAsNoTracking<Video>())
            .Returns(new List<Video>()
            {
                GetVideo()
            }.AsQueryable());
    }

    #region Private Methods

    private Video GetVideo()
    {
        var video = Video.Create(
            Faker.Lorem.Text(),
            Faker.Lorem.Text(),
            Faker.Random.Int(1, 100),
            Faker.Random.Bool(),
            Faker.Random.Bool(),
            Faker.Random.Int(),
            Faker.Random.Enum<Rating>());

        video.Id = Guid.Parse("186184cd-7fc2-4205-a7c3-97eb4ea82b34");

        video.UpdateThumb(Guid.NewGuid().ToString());
        video.UpdateThumbHalf(Guid.NewGuid().ToString());

        return video;
    }

    #endregion
}