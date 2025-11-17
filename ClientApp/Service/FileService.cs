using System.Net.Http.Json;

namespace ClientApp.Service;

public class FileService : IFileService
{
    
    private HttpClient http;

    public FileService(HttpClient http)
    {
        this.http = http;
    }

    
    public async Task<(bool success, string info)> SendFile(string filename, Stream s)
    {
        // add the stream to the body of the http request
        // The DataContent is a kind of envelope for the stream
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(s), "file", filename);

        var response = await http.PostAsync($"{Server.Url}/files/upload", content);
        
        // the response contains the key created by the webservice in case of success.
        // that key must be used when getting the file from the API.
        string key = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return (true, key);
        }
        // else
        return (false, response.ReasonPhrase);

    }

    public async Task<List<string>> GetAllKeys()
    {
        var keys = await http.GetFromJsonAsync<List<string>>($"{Server.Url}/files/getall");
        return keys;
    }

    public string ConvertToUrl(string key) => $"{Server.Url}/files/{key}";
    
    
    public async Task<(bool success, string info)> DeleteFile(string filename)
    {
        var httpResp = await http.DeleteAsync($"{Server.Url}/files/{filename}");
        if (httpResp.IsSuccessStatusCode)
        {
            return (true, "File deleted");
        }
        return (false, httpResp.ReasonPhrase);
        

    }
}