namespace ClientApp.Service;

public interface IFileService
{
    // represent a collection of files containing images.
    // when a file is added, a unique key is generated and returned.
    
    // send s as a file. [success] is true if the file was saved and then the info is the
    // unique key for that file. [success] is false if an error occured, and then the info 
    // is the errormessage.
    Task<(bool success, string info)> SendFile(string filename, Stream s);
    
    // Get keys for all files 
    Task<List<string>> GetAllKeys();
    
    // expand/convert a key for file to an absolute URL for the file
    string ConvertToUrl(string key);

    Task<(bool success, string info)> DeleteFile(string filename);
}