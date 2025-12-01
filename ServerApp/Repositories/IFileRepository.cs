namespace ServerApp.Repositories;

public interface IFileRepository
{
    // repesent a repo of files. When a file is added, it is given a unique name
    // to retreive the content of a file in the repo, you must provide its name.
    
    
    // will add [file] to the repo - return the unique name it is given
   Task<string> UploadAsync(IFormFile file);
   // Get all names of files in the repo
   Task<List<string>> GetAllAsync();

   // get the content of file named [fileName] as a stream, the content-type
   Task<(Stream Stream, string ContentType)?> GetStreamAsync(
       string fileName);

   // delete the file named [fileName]. Return true if success, false otherwise.
   Task<bool> DeleteAsync(string fileName);
}