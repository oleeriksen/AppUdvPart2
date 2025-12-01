using Azure.Storage.Blobs.Models;

namespace ServerApp.Repositories;
using Azure.Storage.Blobs;

public class FileRepository
{
    private readonly BlobContainerClient _container;

    public FileRepository(IConfiguration config)
    {
        var connStr = config["AzureStorage:ConnectionString"];
        var containerName = config["AzureStorage:ContainerName"];
        _container = new BlobContainerClient(connStr, containerName);
        _container.CreateIfNotExists();
    }

    public async Task<string> UploadAsync(IFormFile file, CancellationToken ct = default)
    {
        var blobName = $"{Guid.NewGuid()}_{file.FileName}";
        var blobClient = _container.GetBlobClient(blobName);

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: false, cancellationToken: ct);

        return blobClient.Uri.ToString(); // store this URL in DB if needed
    }

    public async Task<List<string>> GetAll()
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
    
    public async Task<(Stream Stream, string ContentType, string FileName)?> GetBlobStreamAsync(
        string blobName,
        CancellationToken ct = default)
    {
        var blobClient = _container.GetBlobClient(blobName);

        if (!await blobClient.ExistsAsync(ct))
            return null;

        // Get properties for content type / metadata
        BlobProperties props = await blobClient.GetPropertiesAsync(cancellationToken: ct);

        var stream = await blobClient.OpenReadAsync(cancellationToken: ct);
        var contentType = props.ContentType ?? "application/octet-stream";
        var fileName = blobClient.Name; // or use metadata

        return (stream, contentType, fileName);
    }
    
    public async Task<bool> DeleteBlobAsync(string blobName, CancellationToken ct = default)
    {
        var blobClient = _container.GetBlobClient(blobName);

        // DeleteIfExistsAsync returns true/false
        var result = await blobClient.DeleteIfExistsAsync(
            Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots,
            cancellationToken: ct);

        return result.Value; // true if deleted, false if blob didn’t exist
    }
}