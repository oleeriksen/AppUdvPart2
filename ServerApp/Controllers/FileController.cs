using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories;

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

    // provide fileupload - the file is copied to the PATH and given
    // a unique filename with the same extension as the uploaded file. 
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var url = await mFileRep.UploadAsync(file);
        return Ok(new { url });
    }
    
    
    [HttpGet("download/{blobName}")]
    public async Task<IActionResult> GetByBlobName(string blobName)
    {
        var result = await mFileRep.GetStreamAsync(blobName);

        if (result is null)
            return NotFound();

        var (stream, contentType) = result.Value;

        // File(...) will take care of streaming the response
        return File(stream, contentType);
    }
    

    // Return a list af filenames for all the visible files in the PATH folder. 
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