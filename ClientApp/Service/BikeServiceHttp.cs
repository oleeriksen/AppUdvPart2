using System.Net.Http.Json;
using Core;

namespace ClientApp.Service;

public class BikeServiceHttp : IBikeService
{
    
    private HttpClient client;
    
    public BikeServiceHttp(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Bike[]?> GetAll()
    {
        return await client.GetFromJsonAsync<Bike[]>($"{Server.Url}/bike");
    }

    public async Task Add(Bike bike)
    {
        await client.PostAsJsonAsync($"{Server.Url}/bike", bike);
    }

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{Server.Url}/bike/{id}");
    }
}