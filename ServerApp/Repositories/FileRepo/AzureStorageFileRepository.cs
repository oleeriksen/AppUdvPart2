using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace ServerApp.Repositories.FileRepo;


public class AzureStorageFileRepository : IFileRepository
{
    private readonly BlobContainerClient _container;

    public AzureStorageFileRepository(IConfiguration config)
    {
        var connStr = config["AzureStorage:ConnectionString"];
        var containerName = config["AzureStorage:ContainerName"];
        _container = new BlobContainerClient(connStr, containerName);
        _container.CreateIfNotExists();
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var blobName = $"{Guid.NewGuid()}_{file.FileName}";
        var blobClient = _container.GetBlobClient(blobName);

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: false);

        return blobName;
    }

    public async Task<List<string>> GetAllAsync()
    {
        List<string> blobNames = new();

        await foreach (var blobItem in _container.GetBlobsAsync())
        {
            //var blobClient = _container.GetBlobClient(blobItem.Name);
            //urls.Add(blobClient.Uri.ToString());
            blobNames.Add(blobItem.Name);
        }

        return blobNames;
    }
    
    public async Task<(Stream Stream, string ContentType)?> GetStreamAsync(string blobName)
    {
        var blobClient = _container.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync())
            return null;

        // Get properties for content type / metadata
        BlobProperties props = await blobClient.GetPropertiesAsync();

        var stream = await blobClient.OpenReadAsync();
        var contentType = props.ContentType ?? "application/octet-stream";

        return (stream, contentType);
    }
    

    public async Task<bool> DeleteAsync(string blobName)
    {
        var blobClient = _container.GetBlobClient(blobName);

        // DeleteIfExistsAsync returns true/false
        var result = await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

        return result.Value; // true if deleted, false if blob didn’t exist
    }
}