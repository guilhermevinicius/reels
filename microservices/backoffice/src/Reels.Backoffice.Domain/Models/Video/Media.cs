using Reels.Backoffice.Domain.Models.Video.Enums;
using Reels.Backoffice.Domain.SeedWorks;

namespace Reels.Backoffice.Domain.Models.Video;

public class Media : Entity
{
    public string FilePath { get; private set; }
    public string? EncodedPath { get; private set; }
    public MediaStatus Status { get; private set; } = MediaStatus.Pending;

    private Media(string filePath, string? encodedPath)
    {
        FilePath = filePath;
        EncodedPath = encodedPath;
    }

    public static Media Create(string filePath, string? encodedPath)
    {
        return new Media(filePath, encodedPath);
    }

    public void UpdateAsSentToEncode()
    {
        Status = MediaStatus.Processing;
    }

    public void UpdateAsEncoded(string encodedExamplePath)
    {
        Status = MediaStatus.Completed;
        EncodedPath = encodedExamplePath;
    }

    public void UpdateAsEncodingError()
    {
        Status = MediaStatus.Error;
        EncodedPath = null;
    }
}