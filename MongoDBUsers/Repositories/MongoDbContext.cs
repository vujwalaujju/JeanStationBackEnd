using MongoDB.Driver;
using MongoDbDemo.Models;

namespace MongoDbDemo.Services
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("JeanStation");
        }

        public IMongoCollection<SignUp> Users => _database.GetCollection<SignUp>("Users");
    }
}
