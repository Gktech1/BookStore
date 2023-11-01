using Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings")["ConnectionString"]);
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings")["DatabaseName"]);

            Products = database.GetCollection<Product>(configuration.GetSection("DatabaseSettings")["CollectionName"]);
            ProductContextSeed.SeedData(Products);
        }
         
        public IMongoCollection<Product> Products { get; }
    }
}
