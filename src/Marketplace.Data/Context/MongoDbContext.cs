using Marketplace.Configuration;
using Marketplace.Domain.Events;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Marketplace.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        public IMongoDatabase Db { get; }
        public MongoClient Client { get; }
        public IDomainEventDispatcher EventDispatcher { get; }

        public MongoDbContext(IOptions<MongoDbOptions> options, IDomainEventDispatcher eventDispatcher)
        {
            if (Client == null)
                Client = new MongoClient(options.Value.ConnectionString);

            if (Client != null && Db == null)
                Db = Client.GetDatabase(options.Value.Database);

            EventDispatcher = eventDispatcher;
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return Db.GetCollection<T>(typeof(T).Name);
        }
    }
}