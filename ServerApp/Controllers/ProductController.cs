using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories.ProductRepo;
using Core;

namespace ServerApp.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{

    private IProductRepository productRepo;

    public ProductController(IProductRepository productRepo) {
        this.productRepo = productRepo;
    }

    [HttpGet]
    public List<Product> Get()
    {
        return productRepo.GetAll();
    }

    [HttpPost]
    public void Add(Product p) {
        productRepo.Add(p);
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    public void Delete(int id)
    {
        productRepo.DeleteById(id);
    }
    
    [HttpDelete]
    [Route("delete")]
    public void DeleteByQuery([FromQuery]int id)
    {
        productRepo.DeleteById(id);
    }
}