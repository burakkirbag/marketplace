using Marketplace.Domain.Events;
using MongoDB.Driver;

namespace Marketplace.Data
{
    public interface IMongoDbContext
    {
        IMongoDatabase Db { get; }
        MongoClient Client { get; }
        IDomainEventDispatcher EventDispatcher { get; }
        IMongoCollection<T> GetCollection<T>();
    }
}