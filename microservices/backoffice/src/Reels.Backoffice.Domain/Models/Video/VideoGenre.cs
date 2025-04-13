using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Video;

public class VideoGenre : Entity
{
    public Guid VideoId { get; private set; }
    public Guid GenreId { get; private set; }

    private VideoGenre(Guid videoId, Guid genreId)
    {
        VideoId = videoId;
        GenreId = genreId;
    }

    public static VideoGenre Create(Guid videoId, Guid genreId)
    {
        return new VideoGenre(videoId, genreId);
    }
}