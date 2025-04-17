namespace Reels.Backoffice.Infrastructure.Storage;

public sealed record StorageSettings
{
    public string Endpoint { get; init; }
    public string AccessKey { get; init; }
    public string SecretKey { get; init; }
    public string BucketName { get; init; }
}