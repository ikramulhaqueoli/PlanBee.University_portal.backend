using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities;
using System.Reflection;

namespace PlanBee.University_portal.backend.Repositories.Implementations
{
    public class MongoRepository : IMongoReadRepository, IMongoWriteRepository
    {
        private readonly IMongoDbCollectionProvider _mongoDbCollectionProvider;

        public MongoRepository(IMongoDbCollectionProvider mongoDbCollectionProvider)
        {
            _mongoDbCollectionProvider = mongoDbCollectionProvider;
        }

        public Task DeleteAsync<T>(FilterDefinition<T> filter) where T : EntityBase
        {
            return _mongoDbCollectionProvider.getCollection<T>().DeleteManyAsync(filter);
        }

        public async Task<List<T>> GetAsync<T>(FilterDefinition<T> filter, bool excludeMarkedAsDeleted = true) where T : EntityBase
        {
            var filterExcludeMarkedAsDeleted = Builders<T>.Filter.Eq(nameof(EntityBase.IsMarkedAsDeleted), false);
            var finalFilter = excludeMarkedAsDeleted
                ? filter & filterExcludeMarkedAsDeleted
                : filter;
            var results = await _mongoDbCollectionProvider.getCollection<T>().FindAsync(finalFilter);
            return results.ToList();
        }

        public Task<long> GetCountAsync<T>(FilterDefinition<T> filter, bool excludeMarkedAsDeleted = true) where T : EntityBase
        {
            var filterExcludeMarkedAsDeleted = Builders<T>.Filter.Eq(nameof(EntityBase.IsMarkedAsDeleted), false);
            var finalFilter = excludeMarkedAsDeleted
                ? filter & filterExcludeMarkedAsDeleted
                : filter;
            return _mongoDbCollectionProvider.getCollection<T>().CountDocumentsAsync(finalFilter);
        }

        public async Task<T?> GetFirstOrDefaultAsync<T>(FilterDefinition<T> filter, bool excludeMarkedAsDeleted = true) where T : EntityBase
        {
            var filterExcludeMarkedAsDeleted = Builders<T>.Filter.Eq(nameof(EntityBase.IsMarkedAsDeleted), false);
            var finalFilter = excludeMarkedAsDeleted 
                ? filter & filterExcludeMarkedAsDeleted
                : filter;
            return await _mongoDbCollectionProvider.getCollection<T>().Find(finalFilter).FirstOrDefaultAsync();
        }

        public Task SaveAsync<T>(T item) where T : EntityBase
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return _mongoDbCollectionProvider.getCollection<T>().InsertOneAsync(item);
        }

        public Task UpdateAsync<T>(T item) where T : EntityBase
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            item.Modify();

            var filter = Builders<T>.Filter.Eq(nameof(EntityBase.ItemId), item.ItemId);

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var updateDefinition = Builders<T>.Update.Combine(properties
                .Where(prop => prop.Name != nameof(EntityBase.ItemId))
                .Select(prop => Builders<T>.Update.Set(prop.Name, prop.GetValue(item))));

            return _mongoDbCollectionProvider.getCollection<T>().UpdateOneAsync(filter, updateDefinition);
        }
    }
}
