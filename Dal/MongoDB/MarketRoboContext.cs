using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AV.Development.Dal.MongoDB
{
    public class MarketRoboContext
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private const string _databaseName = "MarketRobo";
        private static MarketRoboContext _loadTestingContext;

        private MarketRoboContext() { }

        public static MarketRoboContext Create(IMongoConnectionStringRepository connectionStringRepository)
        {
            if (_loadTestingContext == null)
            {
                _loadTestingContext = new MarketRoboContext();
                string connectionString = connectionStringRepository.ReadConnectionString("MongoDbMarketRoboContext");
                _loadTestingContext.Client = new MongoClient(connectionString);
                _loadTestingContext.Database = _loadTestingContext.Client.GetDatabase(_databaseName);
            }
            return _loadTestingContext;
        }

        public IMongoCollection<EntityMongoDao> Entities
        {
            get { return Database.GetCollection<EntityMongoDao>("Entities"); }
        }


        public IMongoCollection<MetadataVersionMongoDao> MetadataVersion(string collectionNames)
        {
            return Database.GetCollection<MetadataVersionMongoDao>(collectionNames);
        }

        public async void CollectionBsonDocument(string collectionName)
        {
            var collection = Database.GetCollection<BsonDocument>(collectionName);
            var filter = new BsonDocument();
            var count = 0;
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {

                        // process document
                        count++;
                    }
                }
            }
        }
    }
}
