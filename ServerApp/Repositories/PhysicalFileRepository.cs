namespace ServerApp.Repositories;

public class PhysicalFileRepository : IFileRepository
{
    private string PATH = "/Users/oleeriksen/Data/Files/uploads";
    // here files will be stored
    
    public async Task<string> UploadAsync(IFormFile file)
    {
        // ensure the folder is there
        if (!Directory.Exists(PATH))
            Directory.CreateDirectory(PATH);

        // compute a unique new filename
        var fileName = UniqueFilename() + Path.GetExtension(file.FileName);
        var path = Path.Combine(PATH, fileName);
        
        await using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }

    // return a unique filename - uses the Tick property from DateTime
    private string UniqueFilename()
    {
        DateTime now = DateTime.Now;
        return now.Ticks.ToString();
    }
    
    public async Task<List<string>> GetAllAsync()
    {
        List<string> res = new();
        DirectoryInfo folder = new DirectoryInfo(PATH);
        foreach (var f in folder.EnumerateFiles())
        {
            if (! f.Name.StartsWith('.')) // hidden files
                res.Add(f.Name);
        }
        return res;
    }
    
    public async Task<(Stream Stream, string ContentType)?> GetStreamAsync(string fileName)
    {
        var filePath = Path.Combine(PATH, fileName);

        if (!System.IO.File.Exists(filePath))
            return null;

        var mimeType = GetMimeType(filePath); // see below
        
        var stream  = File.Open(filePath, FileMode.Open);
        
        return (stream, mimeType);
    }
    
    private string GetMimeType(string filePath)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            // set type of content to unknown...
            contentType = "application/octet-stream";
        }
        return contentType;
    }

    public async Task<bool> DeleteAsync(string fileName)
    {
        var filePath = Path.Combine(PATH, fileName);

        bool result;
        if (!File.Exists(filePath))
            result = false;
        else
        {
            File.Delete(filePath);
            result = true;
        }

        return result;
    }
}