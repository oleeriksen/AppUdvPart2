using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private IFileRepo mRepo;
    private string PATH = "/Users/oleeriksen/Data/Files/uploads";
    // here files will be stored

    public FileController(IFileRepo repo)
    {
        mRepo = repo;
    }

    // provide fileupload - the file is copied to the PATH and given
    // a unique filename with the same extension as the uploaded file. 
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        // if no or empty file - return bad request
        if (file == null || file.Length == 0)
            return BadRequest("Ingen fil modtaget");

        var fileName = await mRepo.AddFile(file);
        return Ok(fileName);
    }
    

    // Return the file with name [fileName]. This is regarded as a key for the
    // file
    [HttpGet]
    [Route("{fileName}")]
    public async Task<IActionResult> GetFileByKey(string fileName)
    {

        var res = await mRepo.GetFileContent(fileName);
        if (res.success)
        {
            var mimeType = GetMimeType(res.path); // see below

            return PhysicalFile(res.path, mimeType);
        }
        return NotFound();
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

    // Return a list af filenames for all the visible files in the PATH folder. 
    [HttpGet]
    [Route("getall")]
    public List<string> GetAll()
    {
        return mRepo.GetAllFilenames();
    }
    
    // Return the file with name [fileName]. This is regarded as a key for the
    // file
    [HttpDelete]
    [Route("{fileName}")]
    public IActionResult DeleteFileByKey(string fileName)
    {
        var result = mRepo.DeleteFile(fileName);
        if (!result)
            return NotFound();
        return Ok();
    }
}