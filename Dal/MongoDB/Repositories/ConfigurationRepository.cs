using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Repositories.Interface;

namespace AV.Development.Dal.MongoDB.Repositories
{

    public class ConfigurationRepository : MongoDbRepository, IConfigurationRepository
    {
        public ConfigurationRepository(IConnectionStringRepository connectionStringRepository)
            : base(connectionStringRepository)
        { }

        public IList<EntityMongoDao> GetEntities(int pageNo, int pageSize)
        {
            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            List<EntityMongoDao> mongoDbLoadEntitesInSearchPeriod = context.Entities.Find(x => true)
                .ToList();

            return mongoDbLoadEntitesInSearchPeriod;
        }

        public void AddOrUpdateLoadEntites(List<EntityMongoDao> ToBeInserted = null, List<EntityMongoDao> ToBeUpdated = null)
        {
            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);

            if (ToBeInserted.Any())
            {

                context.Entities.InsertMany(ToBeInserted);
            }

            if (ToBeUpdated.Any())
            {
                foreach (EntityMongoDao toBeUpdated in ToBeUpdated)
                {
                    int existingEntityId = toBeUpdated.EntityId;
                    var loadentityInDbQuery = context.Entities.Find<EntityMongoDao>(lt => lt.EntityId == existingEntityId);
                    EntityMongoDao loadentityInDb = loadentityInDbQuery.SingleOrDefault();
                    loadentityInDb.Name = toBeUpdated.Name;
                    loadentityInDb.CreationDate = toBeUpdated.CreationDate;
                    context.Entities.FindOneAndReplace<EntityMongoDao>(lt => lt.DbObjectId == loadentityInDb.DbObjectId, loadentityInDb);
                }
            }
        }



        public void DeleteById(int id)
        {
            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            context.Entities.FindOneAndDelete<EntityMongoDao>(lt => lt.EntityId == id);
        }
    }
}
