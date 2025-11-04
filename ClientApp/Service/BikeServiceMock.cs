using Core;

namespace ClientApp.Service;

public class BikeServiceMock : IBikeService
{
    private List<Bike> mBikes;

    public BikeServiceMock()
    {
        mBikes = new();
    }
    
    public async Task<Bike[]> GetAll()
    {
        return mBikes.ToArray();
    }

    public async Task Add(Bike bike)
    {
        int max = 0;
        if (mBikes.Count > 0)
            max = mBikes.Select(b => b.Id).Max();
        bike.Id = max + 1;
        mBikes.Add(bike);
    }

    public async Task DeleteById(int id)
    {
        mBikes.RemoveAll(bike => bike.Id == id);
    }
}