using MongoDB.Driver;
using PlanBee.University_portal.backend.Domain.Entities;

namespace PlanBee.University_portal.backend.Repositories
{
    internal interface IMongoDbCollectionProvider
    {
        IMongoCollection<T> getCollection<T>() where T : EntityBase;
    }
}