using System.Net.Http.Json;
using Core;

namespace ClientApp.Service;

public class BikeServiceHttp : IBikeService
{
    private string serverUrl = "http://localhost:5217/api/bike";
    
    private HttpClient client;
    
    public BikeServiceHttp(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Bike[]> GetAll()
    {
        return await client.GetFromJsonAsync<Bike[]>(serverUrl);
    }

    public async Task Add(Bike bike)
    {
        await client.PostAsJsonAsync(serverUrl, bike);
    }

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}/{id}");
    }
}