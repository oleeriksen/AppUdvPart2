using Core;

namespace ServerApp.Repositories.ProductRepo;

public class ProductRepositoryMock : IProductRepository
{
    private List<Product> mProducts = new();

    public List<Product> GetAll() => mProducts;

    public void Add(Product p)
    {
        mProducts.Add(p);
    }

    public void DeleteById(int id)
    {
        mProducts.RemoveAll(p => p.Id == id);
    }
}