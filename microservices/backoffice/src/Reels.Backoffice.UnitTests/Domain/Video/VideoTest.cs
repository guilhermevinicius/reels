using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.UnitTests.Common;
using Entity = Reels.Backoffice.Domain.Models.Video;

namespace Reels.Backoffice.UnitTests.Domain.Video;

public class VideoTest : BaseTest
{
    [Fact]
    public void Video_Create_ShouldBeCreated()
    {
        // Arrange
        var title = Faker.Random.String(10);
        var description = Faker.Random.String(10);
        var yearLaunched = Faker.Random.Int();
        var opened = Faker.Random.Bool();
        var published = Faker.Random.Bool();
        var duration = Faker.Random.Int();
        var rating = Faker.Random.Enum<Rating>();

        // Action
        var video = Entity.Video.Create(title, description, yearLaunched, opened, published, duration, rating);

        // Assert
        Assert.Equal(title, video.Title);
        Assert.Equal(description, video.Description);
        Assert.Equal(yearLaunched, video.YearLaunched);
        Assert.Equal(opened, video.Opened);
        Assert.Equal(published, video.Published);
        Assert.Equal(duration, video.Duration);
        Assert.Equal(rating, video.Rating);
    }

    [Fact]
    public void Video_Update_ShouldBeUpdated()
    {
        // Arrange
        var title = Faker.Random.String(10);
        var description = Faker.Random.String(10);
        var yearLaunched = Faker.Random.Int();
        var opened = Faker.Random.Bool();
        var published = Faker.Random.Bool();
        var duration = Faker.Random.Int();
        var rating = Faker.Random.Enum<Rating>();
        var video = GetVideo();

        // Action
        video.Update(title, description, yearLaunched, opened, published, duration, rating);

        // Assert
        Assert.Equal(title, video.Title);
        Assert.Equal(description, video.Description);
        Assert.Equal(yearLaunched, video.YearLaunched);
        Assert.Equal(opened, video.Opened);
        Assert.Equal(published, video.Published);
        Assert.Equal(duration, video.Duration);
        Assert.Equal(rating, video.Rating);
    }

    [Fact]
    public void Video_UpdateThumb_ShouldNotBeUpdateThumb()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();

        // Action
        video.UpdateThumb(path);

        // Arrange
        Assert.NotNull(video.Thumb);
        Assert.Equal(path, video.Thumb.Path);
    }

    [Fact]
    public void Video_UpdateThumbHalf_ShouldNotBeUpdateThumbHalf()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();

        // Action
        video.UpdateThumbHalf(path);

        // Arrange
        Assert.NotNull(video.ThumbHalf);
        Assert.Equal(path, video.ThumbHalf.Path);
    }

    [Fact]
    public void Video_UpdateBanner_ShouldNotBeUpdateBanner()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();

        // Action
        video.UpdateBanner(path);

        // Arrange
        Assert.NotNull(video.Banner);
        Assert.Equal(path, video.Banner.Path);
    }

    [Fact]
    public void Video_UpdateMedia_ShouldNotBeUpdateMedia()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();

        // Action
        video.UpdateMedia(path);

        // Arrange
        Assert.NotNull(video.Media);
        Assert.Equal(path, video.Media.FilePath);
    }
    
    [Fact]
    public void Video_UpdateTrailer_ShouldNotBeUpdateTrailer()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();

        // Action
        video.UpdateTrailer(path);

        // Arrange
        Assert.NotNull(video.Trailer);
        Assert.Equal(path, video.Trailer.FilePath);
    }
    
    [Fact]
    public void Video_Media_ShouldBeUpdateAsSentToEncode()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();
        video.UpdateMedia(path);

        // Action
        video.UpdateAsSentToEncode();

        // Arrange
        Assert.NotNull(video.Media);
        Assert.Equal(MediaStatus.Processing, video.Media.Status);
    }
    
    [Fact]
    public void Video_Media_ShouldBeUpdateAsEncoded()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();
        video.UpdateMedia(path);

        // Action
        video.UpdateAsEncoded(path);

        // Arrange
        Assert.NotNull(video.Media);
        Assert.Equal(path, video.Media.EncodedPath);
        Assert.Equal(MediaStatus.Completed, video.Media.Status);
    }

    [Fact]
    public void Video_Media_ShouldBeUpdateAsEncodingError()
    {
        // Arrange
        var path = Faker.Random.String(10);
        var video = GetVideo();
        video.UpdateMedia(path);

        // Action
        video.UpdateAsEncodingError();

        // Arrange
        Assert.NotNull(video.Media);
        Assert.Equal(MediaStatus.Error, video.Media.Status);
    }
    
    [Fact]
    public void Video_Category_ShouldBeAddCategory()
    {
        // Arrange
        var categoryId = Faker.Random.Guid();
        var video = GetVideo();

        // Action
        video.AddCategory(categoryId);

        // Arrange
        Assert.NotEmpty(video.Categories);
    }

    [Fact]
    public void Video_Category_ShouldBeRemoveCategory()
    {
        // Arrange
        var categoryId = Faker.Random.Guid();
        var video = GetVideo();

        // Action
        video.AddCategory(categoryId);
        video.RemoveCategory(categoryId);

        // Arrange
        Assert.Empty(video.Categories);
    }

    [Fact]
    public void Video_Category_ShouldBeRemoveAllCategory()
    {
        // Arrange
        var categoryId = Faker.Random.Guid();
        var video = GetVideo();
        video.AddCategory(categoryId);
        video.AddCategory(categoryId);

        // Action
        video.RemoveAllCategories();

        // Arrange
        Assert.Empty(video.Categories);
    }
    
    [Fact]
    public void Video_Genre_ShouldBeAddGenre()
    {
        // Arrange
        var genreId = Faker.Random.Guid();
        var video = GetVideo();

        // Action
        video.AddGenre(genreId);

        // Arrange
        Assert.NotEmpty(video.Genres);
    }

    [Fact]
    public void Video_Genre_ShouldBeRemoveGenres()
    {
        // Arrange
        var genreId = Faker.Random.Guid();
        var video = GetVideo();

        // Action
        video.AddGenre(genreId);
        video.RemoveGenre(genreId);

        // Arrange
        Assert.Empty(video.Genres);
    }

    [Fact]
    public void Video_Genre_ShouldBeRemoveAllGenre()
    {
        // Arrange
        var genreId = Faker.Random.Guid();
        var video = GetVideo();
        video.AddGenre(genreId);
        video.AddGenre(genreId);

        // Action
        video.RemoveAllGenres();

        // Arrange
        Assert.Empty(video.Genres);
    }

    #region Private Methods

    private Entity.Video GetVideo()
    {
        return Entity.Video.Create(
            Faker.Random.String(10),
            Faker.Random.String(10),
            Faker.Random.Int(),
            Faker.Random.Bool(),
            Faker.Random.Bool(),
            Faker.Random.Int(),
            Faker.Random.Enum<Rating>());
    }

    #endregion
}