using System;
using Core;

namespace ServerApp.Repositories
{
	public class BikeRepositoryInMemory : IBikeRepository
	{

        private static Bike[] bikes = { new Bike { Id = 1, Brand = "TREK", Model="Modane",Description="", Price=40000, ImageUrl="https://trek.scene7.com/is/image/TrekBicycleProducts/DomaneSL6eTap_23_36152_A_Primary?$responsive-pjpg$&cache=on,on&wid=1920&hei=1440"},
                                        new Bike { Id = 2, Brand = "Cannondale", Model="Super Six", Description="", Price=32000, ImageUrl="https://cykelexperten.dk/media/b2/91/0c/1621238790/es.salsify.com_image_upload_s--ig-E1STy--_gkrk5jfl6olvdl9v8sxb.jpg"},
                                        new Bike { Id = 3, Brand="Canyon", Model="Grail CF SL", Description="", Price=35000, ImageUrl="https://www.canyon.com/dw/image/v2/BCML_PRD/on/demandware.static/-/Sites-canyon-master/default/dwc9f5f669/images/full/full_2022_/2022/full_2022_3095_grail-cf-sl-7_P15_P5.png"},
                                        new Bike { Id = 4, Brand = "Factor", Model="O2", Description="", Price=51000, ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSk5bOFdCSSfbEd944YbqFJObVhL7RlNCq5yA&usqp=CAU"}
        };

        private static List<Bike> mBikes = bikes.ToList();

        
        public void Add(Bike bike)
        {
            mBikes.Add(bike);  
        }

        public void Delete(int id)
        {
            mBikes.RemoveAll((b) => b.Id == id);
        }

        public Bike[] GetAll()
        {
            return mBikes.ToArray();
        }
    }
}

