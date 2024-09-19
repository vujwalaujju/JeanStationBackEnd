namespace MongoDbDemo.Models
{
    public class SignIn
    {
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }  // Include role for validation
    }
}

