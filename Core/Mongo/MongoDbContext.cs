using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace AV.Development.Core.Mongo
{
    public class MongoDbContext 
    {
        public const string CONNECTION_STRING_NAME = "enron";
        public const string DATABASE_NAME = "enron";
        public const string Message_COLLECTION_NAME = "messagescopy_copy1";

        // This is ok... Normally, they would be put into
        // an IoC container.
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static MongoDbContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<messagescopy_copy1> Posts
        {
            get { return _database.GetCollection<messagescopy_copy1>(Message_COLLECTION_NAME); }
        }


    }
}