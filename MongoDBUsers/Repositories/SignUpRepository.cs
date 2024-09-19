using MongoDB.Driver;
using MongoDbDemo.Models;
using MongoDbDemo.Services;


namespace MongoDbDemo.Repositories
{
    public class SignUpRepository
    {
        private readonly IMongoCollection<SignUp> _users;

        public SignUpRepository(MongoDbContext context)
        {
            _users = context.Users;
        }

        public void AddUser(SignUp signUp)
        {
            _users.InsertOne(signUp);
        }

        public List<SignUp> GetUsers()
        {
            return _users.Find(_ => true).ToList();
        }
    }
}
