using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{

    private string PATH = "/Users/oleeriksen/Data/Files/uploads";
    // here files will be stored

    // provide fileupload - the file is copied to the PATH and given
    // a unique filename with the same extension as the uploaded file. 
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        // if no or empty file - return bad request
        if (file == null || file.Length == 0)
            return BadRequest("Ingen fil modtaget");

        // ensure the folder is there
        if (!Directory.Exists(PATH))
            Directory.CreateDirectory(PATH);

        // compute a unique new filename
        var fileName = UniqueFilename() + Path.GetExtension(file.FileName);
        var path = Path.Combine(PATH, fileName);

        await using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok(fileName);
    }

    // return a unique filename - uses the Tick property from DateTime
    private string UniqueFilename()
    {
        DateTime now = DateTime.Now;
        return now.Ticks.ToString();
    }

    // Return the file with name [fileName]. This is regarded as a key for the
    // file
    [HttpGet("{fileName}")]
    public IActionResult GetFileByKey(string fileName)
    {
        var filePath = Path.Combine(PATH, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var mimeType = GetMimeType(filePath); // se nedenfor

        return PhysicalFile(filePath, mimeType);
    }

    private string GetMimeType(string filePath)
    {
        var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        return contentType;
    }

    // Return a list af filenames for all the visible files in the PATH folder. 
    [HttpGet]
    [Route("getall")]
    public List<string> GetAll()
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
}