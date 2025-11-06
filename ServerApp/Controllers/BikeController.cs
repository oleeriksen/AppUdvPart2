using Microsoft.AspNetCore.Mvc;
using ServerApp.Repositories;
using Core;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/bike")]
    public class BikeController : ControllerBase
    {

        private IBikeRepository bikeRepo;

        public BikeController(IBikeRepository bikeRepo) {
            this.bikeRepo = bikeRepo;
        }

        [HttpGet]
        public IEnumerable<Bike> Get()
        {
            return bikeRepo.GetAll();
        }

        [HttpPost]
        public void Add(Bike bike) {
            bikeRepo.Add(bike);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public void Delete(int id)
        {
            bikeRepo.Delete(id);
        }
        
        [HttpDelete]
        [Route("delete")]
        public void DeleteByQuery([FromQuery] int id)
        {
            bikeRepo.Delete(id);
        }



    }
}

