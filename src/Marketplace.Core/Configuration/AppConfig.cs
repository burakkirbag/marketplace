namespace Marketplace.Configuration
{
    public class AppConfig
    {
        public MongoDbOptions MongoDb { get; set; }
        public EmailOptions Email { get; set; }
        public KeysOptions Keys { get; set; }
    }
}