namespace Reels.Backoffice.Application.Contracts.Storage;

public interface IStorageService
{
    Task<string> GetPreSigned(string path);
    Task<string> UploadFile(string path, string contentType, string fileName, Stream file);
    Task<string> GetObjectUrl(string key);
    Task<bool> RemoveObject(string key);
}