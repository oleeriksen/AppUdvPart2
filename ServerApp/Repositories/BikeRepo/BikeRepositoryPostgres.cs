using System;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Core;
using Npgsql;

namespace ServerApp.Repositories.BikeRepo
{
    public class BikeRepositoryPostgres : IBikeRepository
    {
        private const string connectionString = @"Server=127.0.0.1:5432;User Id=oleeriksen;Password=1234;database=Bike";


        public BikeRepositoryPostgres()
        {
        }

        public Bike[] GetAll()
        {
            var result = new List<Bike>();


            using (var mConnection = new NpgsqlConnection(connectionString))
            {
                mConnection.Open();
                var command = mConnection.CreateCommand();
                command.CommandText = @"SELECT * FROM bike";

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
                        {
                            Id = id, Brand = brand, Model = model, Description = desc, Price = price, ImageUrl = imgUrl
                        };
                        result.Add(b);
                    }
                }
            }

            return result.ToArray();
        }

        public void Add(Bike bike)
        {
            using (var mConnection = new NpgsqlConnection(connectionString))
            {
                mConnection.Open();
                var command = mConnection.CreateCommand();

                command.CommandText =
                    "INSERT INTO bike (brand, model, description, price, imageurl) VALUES (@brand, @model, @desc, @price, @imgurl)";

                Console.WriteLine(command.CommandText);
                var paramBrand = command.CreateParameter();
                paramBrand.ParameterName = "brand";
                command.Parameters.Add(paramBrand);
                paramBrand.Value = bike.Brand;

                var paramModel = command.CreateParameter();
                paramModel.ParameterName = "model";
                command.Parameters.Add(paramModel);
                paramModel.Value = bike.Model;

                var paramDesc = command.CreateParameter();
                paramDesc.ParameterName = "desc";
                command.Parameters.Add(paramDesc);
                paramDesc.Value = bike.Description;

                var paramPrice = command.CreateParameter();
                paramPrice.ParameterName = "price";
                command.Parameters.Add(paramPrice);
                paramPrice.Value = bike.Price;

                var paramImg = command.CreateParameter();
                paramImg.ParameterName = "imgurl";
                command.Parameters.Add(paramImg);
                paramImg.Value = bike.ImageUrl;

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var mConnection = new NpgsqlConnection(connectionString))
            {
                mConnection.Open();
                var command = mConnection.CreateCommand();

                command.CommandText = $"DELETE FROM bike WHERE id={id}";
                command.ExecuteNonQuery();
            }
        }
    }
}