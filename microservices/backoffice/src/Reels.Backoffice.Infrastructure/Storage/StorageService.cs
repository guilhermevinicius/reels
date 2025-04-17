using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Reels.Backoffice.Application.Contracts.Storage;

namespace Reels.Backoffice.Infrastructure.Storage;

internal sealed class StorageService 
    : IStorageService
{
    private readonly IMinioClient _client;
    private readonly StorageSettings _settings;
    
    public StorageService(
        IOptions<StorageSettings> storageSettings,
        IMinioClientFactory minioClientFactory)
    {
        _settings = storageSettings.Value;

        var beArgs = new BucketExistsArgs()
            .WithBucket(_settings.BucketName);

        var client = minioClientFactory.CreateClient().WithSSL(false);
        client.BucketExistsAsync(beArgs).ConfigureAwait(false);
        _client = client;
    }

    public async Task<string> GetPreSigned(string path)
    {
        return await _client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(path)
            .WithExpiry(60 * 60));
    }

    public async Task<string> UploadFile(string path, string contentType, string fileName, Stream file)
    {
        var pathWithFileName = $"{path}/{fileName}";
        var args = new PutObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(pathWithFileName)
            .WithStreamData(file)
            .WithContentType(contentType)
            .WithObjectSize(file.Length);

        await _client.PutObjectAsync(args);

        return pathWithFileName;
    }

    public async Task<string> GetObjectUrl(string key)
    {
        return await _client.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(key)
            .WithExpiry(60 * 60));
    }

    public async Task<bool> RemoveObject(string key)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(_settings.BucketName)
            .WithObject(key);

        await _client.RemoveObjectAsync(removeObjectArgs);
        return true;
    }
}