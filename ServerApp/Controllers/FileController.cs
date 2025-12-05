using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories.FileRepo;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{

    private IFileRepository mFileRep;

    public FileController(IFileRepository fileRep)
    {
        mFileRep = fileRep;
    }

    // provide fileupload - the file is added to the repo and given
    // a unique filename with the same extension as the uploaded file. 
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var url = await mFileRep.AddAsync(file);
        return Ok(new { url });
    }
    
    
    // provide download of a specific file in the repo - it will
    // return a stream and a type of content
    [HttpGet("download/{filename}")]
    public async Task<IActionResult> GetByName(string filename)
    {
        var result = await mFileRep.GetStreamAsync(filename);

        if (result is null)
            return NotFound();

        var (stream, contentType) = result.Value;

        // File(...) will take care of streaming the response
        return File(stream, contentType);
    }
    

    // Return a list of names for all files in the repo. 
    [HttpGet]
    [Route("getall")]
    public async Task<List<string>> GetAll()
    {
        var res = await mFileRep.GetAllAsync();
        return res;
    }
    
    [HttpDelete("delete/{blobName}")]
    public async Task<IActionResult> Delete(string blobName)
    {
        var deleted = await mFileRep.DeleteAsync(blobName);

        if (!deleted)
            return NotFound(new { message = "Blob not found." });

        return Ok(new { message = "Blob deleted successfully." });
    }
}