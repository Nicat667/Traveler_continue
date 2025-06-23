using Microsoft.AspNetCore.Http;

namespace Service.Services.Interfaces
{
    public interface IBlobStorage
    {
        Task<string> UploadAsync(IFormFile file);
        Task<string> GetBlobUrlAsync(string blobName);
        Task<bool> DeleteAsync(string blobName);
        Task<string> ReplaceAsync(string oldBlobName, IFormFile newFile);
    }
}
