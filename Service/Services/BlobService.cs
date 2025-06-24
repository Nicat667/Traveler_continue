using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Services.Interfaces;
using System.IO;

namespace Service.Services
{
    public class BlobService : IBlobStorage
    {
        private readonly BlobContainerClient _container;
        public BlobService(IConfiguration config)
        {
            var conn = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];
            _container = new BlobContainerClient(conn, containerName);
            _container.CreateIfNotExists();
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var blob = _container.GetBlobClient(fileName);
            await using var stream = file.OpenReadStream();
            await blob.UploadAsync(stream, overwrite: true);

            return fileName;
        }

        public async Task<string> GetBlobUrlAsync(string blobName)
        {
            if (blobName.StartsWith("https://"))
            {
                return blobName;
            }
                
            return _container.GetBlobClient(blobName).Uri.ToString();
        }

        public async Task<bool> DeleteAsync(string blobName)
        {
            return await _container.GetBlobClient(blobName).DeleteIfExistsAsync();
        }

        public async Task<string> ReplaceAsync(string oldBlobName, IFormFile newFile)
        {
            await DeleteAsync(oldBlobName);
            return await UploadAsync(newFile);
        }
    }
}
