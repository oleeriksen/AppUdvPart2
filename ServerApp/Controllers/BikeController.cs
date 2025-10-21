﻿using Microsoft.AspNetCore.Mvc;
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



    }
}

