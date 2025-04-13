using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Video;

public class VideoCategory : Entity
{
    public Guid VideoId { get; private set; }
    public Guid CategoryId { get; private set; }

    private VideoCategory(Guid videoId, Guid categoryId)
    {
        VideoId = videoId;
        CategoryId = categoryId;
    }

    public static VideoCategory Create(Guid videoId, Guid categoryId)
    {
        return new VideoCategory(videoId, categoryId);
    }
}