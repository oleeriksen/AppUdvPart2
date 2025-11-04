using Core;

namespace ClientApp.Service;

public interface IBikeService
{
    Task<Bike[]> GetAll();
    Task Add(Bike bike);
    Task DeleteById(int id);

}