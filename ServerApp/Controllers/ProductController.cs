using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories;
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
    [Route("{id:int}")]
    public void Delete(int id)
    {
        productRepo.DeleteById(id);
    }
}