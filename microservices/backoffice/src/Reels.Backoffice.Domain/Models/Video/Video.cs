using Reels.Backoffice.Domain.Exceptions;
using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.Domain.Models.Video.Events;
using Reels.Backoffice.Domain.Models.Video.ValueObjects;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Video;

public class Video : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int YearLaunched { get; private set; }
    public bool Opened { get; private set; }
    public bool Published { get; private set; }
    public int Duration { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public Rating Rating { get; private set; }
    public Image? Thumb { get; private set; }
    public Image? ThumbHalf { get; private set; }
    public Image? Banner { get; private set; }
    public Media? Media { get; private set; }
    public Media? Trailer { get; private set; }
    private List<VideoCategory> _categories = [];
    public IReadOnlyList<VideoCategory> Categories => _categories;
    private List<VideoGenre> _genres = [];
    public IReadOnlyList<VideoGenre> Genres => _genres;

    private Video(string title, string description, int yearLaunched, bool opened, bool published, 
        int duration, Rating rating)
    {
        Title = title;
        Description = description;
        YearLaunched = yearLaunched;
        Opened = opened;
        Published = published;
        Duration = duration;
        Rating = rating;
    }

    public static Video Create(string title, string description, int yearLaunched, bool opened, bool published, 
        int duration, Rating rating)
    {
        return new Video(title, description, yearLaunched, opened, published, duration, rating);
    }

    public void Update(
        string title,
        string description,
        int yearLaunched,
        bool opened,
        bool published,
        int duration,
        Rating? rating = null)
    {
        Title = title;
        Description = description;
        YearLaunched = yearLaunched;
        Opened = opened;
        Published = published;
        Duration = duration;
        if(rating is not null)
            Rating = (Rating) rating;
    }
    
    public void UpdateThumb(string path)
        => Thumb = new Image(path);

    public void UpdateThumbHalf(string path)
        => ThumbHalf = new Image(path);

    public void UpdateBanner(string path)
        => Banner = new Image(path);

    public void UpdateMedia(string path)
    {
        Media = Media.Create(path, null);
        RaiseEvent(new VideoUploadedEvent(Id, path));
    }

    public void UpdateTrailer(string path)
        => Trailer = Media.Create(path, null);

    public void UpdateAsSentToEncode()
    {
        if (Media is null)
            throw new EntityValidationException("There is no Media");
        Media.UpdateAsSentToEncode();
    }
    
    public void UpdateAsEncoded(string validEncodedPath)
    {
        if (Media is null)
            throw new EntityValidationException("There is no Media");
        Media.UpdateAsEncoded(validEncodedPath);
    }

    public void UpdateAsEncodingError()
    {
        if (Media is null)
            throw new Exception("There is no Media");
        Media.UpdateAsEncodingError();
    }

    public void AddCategory(Guid categoryId)
    {
        _categories.Add(VideoCategory.Create(Id, categoryId));   
    }

    public void RemoveCategory(Guid categoryId) =>
        _categories = _categories.Where(x => 
            x.CategoryId != categoryId).ToList();

    public void RemoveAllCategories()
        => _categories = [];

    public void AddGenre(Guid genreId)
        => _genres.Add(VideoGenre.Create(Id, genreId));

    public void RemoveGenre(Guid genreId)
    {
        _genres = _genres.Where(x => 
            x.GenreId != genreId).ToList();
    }

    public void RemoveAllGenres()
        => _genres = [];
}