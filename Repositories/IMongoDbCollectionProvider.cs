using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories
{
    public interface IMongoDbCollectionProvider
    {
        IMongoCollection<T> getCollection<T>() where T : EntityBase;
    }
}