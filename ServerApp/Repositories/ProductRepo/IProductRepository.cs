namespace ServerApp.Repositories.ProductRepo;
using Core;

public interface IProductRepository
{
     List<Product> GetAll();
     void Add(Product p);
     void DeleteById(int id);

}