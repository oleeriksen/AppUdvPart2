namespace ServerApp.Repositories;

public class FileRepoDisk : IFileRepo
{
    private string PATH = "/Users/oleeriksen/Data/Files/uploads";
    // here files will be stored

    public List<string> GetAllFilenames()
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

    public async Task<string> AddFile(IFormFile file)
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

    public async Task<(bool success, string path)> GetFileContent(string filename)
    {
        var filePath = Path.Combine(PATH, filename);

        if (!System.IO.File.Exists(filePath))
            return (false, "File not found");

        var mimeType = GetMimeType(filePath); // see below

        return (true, filePath);
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
    public bool DeleteFile(string filename)
    {
        var filePath = Path.Combine(PATH, filename);

        if (!System.IO.File.Exists(filePath))
            return false;

        System.IO.File.Delete(filePath);
        return true;
    }
}