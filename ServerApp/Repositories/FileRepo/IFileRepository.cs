namespace ServerApp.Repositories.FileRepo;

public interface IFileRepository
{
    // represent a repo of files. When a file is added, it is given a unique name
    // to retrieve the content of a file in the repo, you must provide its name.
    
    
    // will add [file] to the repo - return the unique name it is given
   Task<string> AddAsync(IFormFile file);
   
   // Get all names of files in the repo
   Task<List<string>> GetAllAsync();

   // get the content of file named [fileName] as a stream, the content-type
   Task<(Stream Stream, string ContentType)?> GetStreamAsync(
       string fileName);

   // delete the file named [fileName]. Return true if success, false otherwise.
   Task<bool> DeleteAsync(string fileName);
}