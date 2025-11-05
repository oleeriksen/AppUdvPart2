using System.Net.Http.Json;
using Core;

namespace ClientApp.Service;

public class ProductServiceHttp : IProductService
{
    private string serverUrl = "http://localhost:5217/api/product";
    
    private HttpClient client;
    
    public ProductServiceHttp(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Product>> GetAll()
    {
        return await client.GetFromJsonAsync<List<Product>>(serverUrl);
    }

    public async Task Add(Product p)
    {
        await client.PostAsJsonAsync(serverUrl, p);
    }

    public async Task DeleteById(int id)
    {
        await client.DeleteAsync($"{serverUrl}/{id}");
    }
}