using MongoDB.Driver;
using MongoDbDemo.Models;
using MongoDbDemo.Services;

namespace MongoDbDemo.Repositories
{
    public class SignInRepository
    {
        private readonly IMongoCollection<SignUp> _users;

        public SignInRepository(MongoDbContext context)
        {
            _users = context.Users;
        }

        public SignUp ValidateUser(SignIn signIn)
        {
            return _users.Find(user => user.email == signIn.email && user.password == signIn.password && user.role == signIn.role).FirstOrDefault();
        }
    }
}
