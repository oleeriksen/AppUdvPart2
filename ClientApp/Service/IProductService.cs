using Core;

namespace ClientApp.Service;

public interface IProductService
{
    Task<List<Product>> GetAll();
    Task Add(Product bike);
    Task DeleteById(int id); 
}