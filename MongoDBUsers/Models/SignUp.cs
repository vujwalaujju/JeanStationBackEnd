using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDemo.Models
{
    public class SignUp
    {
        [BsonId]
        public int ? id {  get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phonenumber { get; set; }
        public string role { get; set; }
    }
}
