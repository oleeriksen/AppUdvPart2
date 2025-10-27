using System;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Core;

namespace ServerApp.Repositories
{
    public class BikeRepositorySQLite : IBikeRepository
    {
        private const string connectionString = @"Data Source=//Users/oleeriksen/Data/bikes.db";

        private SqliteConnection mConnection;
        
        public BikeRepositorySQLite() {
            mConnection = new SqliteConnection(connectionString);
            mConnection.Open();
        }

        public Bike[] GetAll()
        {
            var result = new List<Bike>();

            var command = mConnection.CreateCommand();
            command.CommandText = @"SELECT * FROM Bike";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var brand = reader.GetString(1);
                    var model = reader.GetString(2);
                    var desc = reader.GetString(3);
                    var price = reader.GetInt32(4);
                    var imgUrl = reader.GetString(5);

                    Bike b = new Bike
                        { Id = id, Brand = brand, Model = model, Description = desc, Price = price, ImageUrl = imgUrl };
                    result.Add(b);
                }
            }

            return result.ToArray();
        }

        public void Add(Bike bike)
        {
            var command = mConnection.CreateCommand();

            command.CommandText =
                @"INSERT INTO Bike (Brand, Model, Description, Price, ImageUrl) VALUES ($brand, $model, $desc, $price, $imgurl)";
            command.Parameters.AddWithValue("$brand", bike.Brand);
            command.Parameters.AddWithValue("$model", bike.Model);
            command.Parameters.AddWithValue("$desc", bike.Description);
            command.Parameters.AddWithValue("$price", bike.Price);
            command.Parameters.AddWithValue("$imgurl", bike.ImageUrl);
            command.ExecuteNonQuery();
        }
    }
}