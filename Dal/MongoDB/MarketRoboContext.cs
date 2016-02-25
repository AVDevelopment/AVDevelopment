using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Repositories.Interface;
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

        public static MarketRoboContext Create(IConnectionStringRepository connectionStringRepository)
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


        public IMongoCollection<EntityMongoDao> EntitiesVersion(string collectionNames)
        {
            return Database.GetCollection<EntityMongoDao>(collectionNames);
        }
    }
}
