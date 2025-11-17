namespace ServerApp.Repositories;

public interface IFileRepo
{
    List<string> GetAllFilenames();
    Task<string> AddFile(IFormFile file);
    Task<(bool success, string path)> GetFileContent(string filename);
    bool DeleteFile(string filename);
}