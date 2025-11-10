using System.Net.Http.Json;

namespace ClientApp.Service;

public class FileService
{
    private List<string> keys = new();

    private HttpClient http;

    public FileService(HttpClient http)
    {
        this.http = http;
        
    }

    // send s as a file. [success] is true if the file was saved and then the info is the
    // unique key for that file. [success] is false if an error occured, and then the info 
    // is the errormessage.
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
            keys.Add(key);
            return (true, key);
        }
        // else
        return (false, response.ReasonPhrase);

    }

    public async Task<List<string>> GetAllKeys(bool expandToUrls)
    {
        var keys = await http.GetFromJsonAsync<List<string>>($"{Server.Url}/files/getall");

        if (!expandToUrls)
            return keys;
        var res = new List<string>();
        foreach (string k in keys)
            res.Add($"{Server.Url}/files/{k}");
        return res;
    }
}