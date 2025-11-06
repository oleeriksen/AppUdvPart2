using System.Net.Http.Json;
using Core;

namespace ClientApp.Service;

public class ProductServiceHttp : IProductService
{
    
    private HttpClient client;
    
    public ProductServiceHttp(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Product>?> GetAll()
    {
        return await client.GetFromJsonAsync<List<Product>?>($"{Server.Url}/product");
    }

    public async Task Add(Product p)
    {
        await client.PostAsJsonAsync($"{Server.Url}/product", p);
    }

    public async Task DeleteById(int id)
    {
        var endPoint = $"{Server.Url}/delete/product?id={id}";
        await client.DeleteAsync(endPoint);
    }
}