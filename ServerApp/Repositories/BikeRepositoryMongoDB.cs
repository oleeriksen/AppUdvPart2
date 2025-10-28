using Core;
using MongoDB.Driver;

namespace ServerApp.Repositories;

public class BikeRepositoryMongoDB:IBikeRepository
{
    private IMongoClient client;
        
    private IMongoCollection<Bike> bikeCollection;

    public BikeRepositoryMongoDB() {
            // atlas database
            //var password = ""; //add
            //var mongoUri = $"mongodb+srv://olee58:{password}@cluster0.olmnqak.mongodb.net/?retryWrites=true&w=majority";
           
            //local mongodb
            var mongoUri = "mongodb://localhost:27017/";
            
            try
            {
                client = new MongoClient(mongoUri);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
            throw; }

            // Provide the name of the database and collection you want to use.
            var dbName = "myDatabase";
            var collectionName = "bike";

            bikeCollection = client.GetDatabase(dbName)
               .GetCollection<Bike>(collectionName);
        }

        public void Add(Bike item) {
            var max = 0;
            if (bikeCollection.Count(Builders<Bike>.Filter.Empty) > 0)
            {
                max = MaxId();
            }
            item.Id = max + 1;
            // alternative:
            //int newid = Guid.NewGuid().GetHashCode();
            //item.Id = newid;
            bikeCollection.InsertOne(item);
           
        }

        private int MaxId() {
            /*var noFilter = Builders<BEBike>.Filter.Empty;
            var elementWithHighestId = collection.Find(noFilter).SortByDescending(r => r.Id).Limit(1).ToList()[0];
            return elementWithHighestId.Id;*/
            return GetAll().Select(b => b.Id).Max();

        }

        public void DeleteById(int id){
            var deleteResult = bikeCollection
                .DeleteOne(Builders<Bike>.Filter.Where(r => r.Id == id));
        }

        public Bike[] GetAll() {
            var noFilter = Builders<Bike>.Filter.Empty;
           return bikeCollection.Find(noFilter).ToList().ToArray();
        }

        public Bike[] GetAllByShop(string brand) {
            var brandFilter = Builders<Bike>.Filter.Where(r => r.Brand.Equals(brand));
            return bikeCollection.Find(brandFilter).ToList().ToArray();

        }

        public void Update(Bike item)
        {
            var updateDef = Builders<Bike>.Update
                .Set(x => x.Brand, item.Brand)
                .Set(x => x.Model, item.Model)
                .Set(x => x.Price, item.Price)
                .Set(x => x.Description, item.Description)
                .Set(x => x.ImageUrl, item.ImageUrl);
            bikeCollection.UpdateOne(x => x.Id == item.Id, updateDef);
            
        }
 
}