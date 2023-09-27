using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class MongoDbCollectionProvider : IMongoDbCollectionProvider
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoDbCollectionProvider(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<T> getCollection<T>() where T : EntityBase
        {
            var collectionName = $"{typeof(T).Name}s";
            return _mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}
