namespace Marketplace.Configuration
{
    public class MongoDbOptions
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthMechanism { get; set; }
        public string ConnectionString { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
    }
}