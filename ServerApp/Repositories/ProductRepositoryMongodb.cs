using Core;
using MongoDB.Driver;

namespace ServerApp.Repositories;

public class ProductRepositoryMongoDB :IProductRepository
{
        
    private IMongoCollection<Product> productCollection;

    public ProductRepositoryMongoDB() {
            // atlas database
            //var password = ""; //add
            //var mongoUri = $"mongodb+srv://olee58:{password}@cluster0.olmnqak.mongodb.net/?retryWrites=true&w=majority";
           
            //local mongodb
            var mongoUri = "mongodb://localhost:27017/";
            
            MongoClient client;
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
            var collectionName = "product";

            productCollection = client.GetDatabase(dbName)
               .GetCollection<Product>(collectionName);
        }

        public void Add(Product item) {
            // before inserting, a unique id must be found.
            // the first way to do that is by computing the largest
            // id in the collection. The next unique id is this maximal id
            // plus 1.
            var max = 0;
            if (productCollection.CountDocuments(Builders<Product>.Filter.Empty) > 0)
            {
                max = MaxId();
            }
            item.Id = max + 1;
            // alternatively, you can just choose a new Guid - a take
            // the hashcode of that as the new id. This can fail, by
            // it is very unlikely
            //int newid = Guid.NewGuid().GetHashCode();
            //item.Id = newid;
            productCollection.InsertOne(item);
           
        }
        
        private int MaxId()  => GetAll().Select(b => b.Id).Max();

        
        public void DeleteById(int id)
        {
            var deleteResult = productCollection
                .DeleteOne(Builders<Product>.Filter.Where(r => r.Id == id));
            
        }

        public List<Product> GetAll() {
            var noFilter = Builders<Product>.Filter.Empty;
            return productCollection.Find(noFilter).ToList();
        }
 
}