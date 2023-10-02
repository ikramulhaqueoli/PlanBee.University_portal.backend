using MongoDB.Bson;
using MongoDB.Driver;

namespace PlanBee.University_portal.backend.Services.Implementations
{
    public class SeedDataService : ISeedDataService
    {
        private const string SEED_DATA_STR = "seed_data";
        private readonly IMongoDatabase _mongoDatabase;

        public SeedDataService(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task SaveToDbAsync()
        {
            var seedDataParentPath = $"{Directory.GetCurrentDirectory()}//{SEED_DATA_STR}";
            var childDirectories = Directory.GetDirectories(seedDataParentPath);

            foreach (var childDirectory in childDirectories)
            {
                foreach (var file in Directory.GetFiles(childDirectory, "*.json"))
                {
                    var collectionName = Path.GetFileName(childDirectory);
                    var collection = _mongoDatabase.GetCollection<BsonDocument>(collectionName);

                    var bsonDocument = BsonDocument.Parse(File.ReadAllText(file));
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", bsonDocument["_id"]);
                    await collection.ReplaceOneAsync(filter, bsonDocument, new ReplaceOptions { IsUpsert = true });
                }
            }
        }
    }
}
