namespace ServerApp.Repositories;

public interface IFileRepository
{
   Task Add(string filename, Stream content);
   
}