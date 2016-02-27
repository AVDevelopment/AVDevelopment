using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AV.Development.Core;
using AV.Development.Core.Interface;
using AV.Development.Core.Managers;
using AV.Development.Dal.MongoDB;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Repositories;
using AV.Development.Dal.MongoDB.Repositories.Interface;
using Development.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;



namespace AV.Development.Tests.Controllers
{
    [TestClass]
    public class MongoTestControllerTest
    {

        void InitializeAllDevelopmentInstances()
        {

            //intialize all Development tenants and NHibernate mappings and all
            //this is should be called when we want to perform test cases with respect to NHibernate
            DevelopmentManagerFactory.InitializeSystem();

        }


        [TestMethod]
        public void MONGODBTEST()
        {

            //uncomment this when we want perform Nhibernate tests
            //InitializeAllDevelopmentInstances(); // initialize the persistance layer and nhibernate engine 

            //uncomment this when we want to call any managers methods
            // Arrange
            //Guid systemSession = DevelopmentManagerFactory.GetSystemSession();
            //IDevelopmentManager developmentManager = DevelopmentManagerFactory.GetDevelopmentManager(systemSession);


            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());
            IList<EntityMongoDao> loadtestsInPeriod = repo.GetEntities(1, 100);
            List<EntityMongoDao> toBeInserted = new List<EntityMongoDao>();
            List<EntityMongoDao> toBeUpdated = new List<EntityMongoDao>();

            List<AttributeDataMongoDao> attrDao = new List<AttributeDataMongoDao>();
            attrDao.Add(new AttributeDataMongoDao { ID = 1, Caption = "Fiscal Year", Value = 1, ValueCaption = "2016" });
            attrDao.Add(new AttributeDataMongoDao { ID = 2, Caption = "Country", Value = 2, ValueCaption = "India" });

            int entitySize = 10000;

            for (int i = 0; i <= entitySize - 1; i++)
            {

                EntityMongoDao ltNewOne = new EntityMongoDao
                {
                    EntityId = repo.GetRandomNumber(1000, 1000000),
                    Name = repo.GenerateRandomEntityName(),
                    CreationDate = DateTime.Now,
                    AttributeData = attrDao,
                    TypeName = repo.GenRandomEntityTypeName(),
                    TypeId = repo.GenRandomEntityType()
                };
                EntityMongoDao ltNewTwo = new EntityMongoDao
                {
                    EntityId = repo.GetRandomNumber(1000, 1000000),
                    Name = repo.GenerateRandomEntityName(),
                    CreationDate = DateTime.Now,
                    AttributeData = attrDao,
                    TypeName = repo.GenRandomEntityTypeName(),
                    TypeId = repo.GenRandomEntityType()
                };

                toBeInserted.Add(ltNewOne);
                toBeInserted.Add(ltNewTwo);

            }


            EntityMongoDao ltUpdOne = new EntityMongoDao { EntityId = toBeInserted[0].EntityId, Name = repo.GenerateRandomEntityName(), CreationDate = DateTime.Now.AddDays(1), AttributeData = attrDao };
            toBeUpdated.Add(ltUpdOne);
            repo.AddOrUpdateLoadEntites(toBeInserted, toBeUpdated);

            Assert.IsNotNull(toBeInserted[0].DbObjectId); // Test that record saved properly
        }

        [TestMethod]
        public void TestSelectAllNoFilter()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            Task<List<EntityMongoDao>> dbEntitiesTask = context.Entities.Find(x => true).ToListAsync();
            Task.WaitAll(dbEntitiesTask);
            List<EntityMongoDao> dbEntites = dbEntitiesTask.Result;

            foreach (EntityMongoDao eng in dbEntites)
            {
                Debug.WriteLine(eng.Name);
            }

            Assert.AreNotEqual(0, dbEntites.Count); // Test that find worked properly or not
        }

        [TestMethod]
        public void TestSelectWithWhereClause()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            Task<List<EntityMongoDao>> planEntitiesTask = context.Entities.Find(a => a.TypeName == "Plan").ToListAsync();
            Task.WaitAll(planEntitiesTask);
            List<EntityMongoDao> planEntites = planEntitiesTask.Result;

            foreach (EntityMongoDao agent in planEntites)
            {
                Debug.WriteLine(agent.TypeName);
            }

            int entityID = 96;
            Task<EntityMongoDao> singleAgentByIdTask = context.Entities.Find(a => a.EntityId == entityID).SingleOrDefaultAsync();
            Task.WaitAll(singleAgentByIdTask);
            EntityMongoDao singleEntityById = singleAgentByIdTask.Result;
            if (singleEntityById != null)
                Debug.WriteLine(singleEntityById.TypeName);

            Task<List<EntityMongoDao>> planEntitiesWithBuilderTask = context.Entities
                .Find(Builders<EntityMongoDao>.Filter.Eq<string>(a => a.TypeName, "Plan")).ToListAsync();
            Task.WaitAll(planEntitiesWithBuilderTask);
            List<EntityMongoDao> planEntitesWithBuilder = planEntitiesWithBuilderTask.Result;

            foreach (EntityMongoDao entity in planEntitesWithBuilder)
            {
                Debug.WriteLine(entity.TypeName);
            }

            Assert.AreNotEqual(0, planEntitesWithBuilder.Count); // Test that where clause worked properly or not
        }

        [TestMethod]
        public void TestEntityInsertion()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());
            List<AttributeDataMongoDao> attrDao = new List<AttributeDataMongoDao>();
            attrDao.Add(new AttributeDataMongoDao { ID = 1, Caption = "Fiscal Year", Value = 1, ValueCaption = "2016" });
            attrDao.Add(new AttributeDataMongoDao { ID = 2, Caption = "Country", Value = 2, ValueCaption = "India" });
            EntityMongoDao newEntity = new EntityMongoDao()
            {
                EntityId = repo.GetRandomNumber(1, 100),
                Name = repo.GenerateRandomEntityName(),
                CreationDate = DateTime.Now,
                AttributeData = attrDao,
                TypeName = "Plan",
                TypeId = 1
            };
            Task insertionTask = context.Entities.InsertOneAsync(newEntity);
            Task.WaitAll(insertionTask);

            Assert.IsNotNull(newEntity.DbObjectId); // Test that insertion happened properly or not
        }

        [TestMethod]
        public void TestReplacement()
        {
            string beforeReplacement = "", afterReplacement = "";

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());
            int existingEntityId = 96;
            Task<EntityMongoDao> entityTask = context.Entities.Find(p => p.EntityId == existingEntityId).SingleOrDefaultAsync();
            Task.WaitAll(entityTask);
            EntityMongoDao entity = entityTask.Result;
            if (entity != null)
            {
                beforeReplacement = entity.Name;
                entity.Name = repo.GenerateRandomEntityName();
                entity.UniqueKey = Convert.ToString(repo.GetRandomNumber(1, 100));
                Task<EntityMongoDao> replacementTask = context.Entities.FindOneAndReplaceAsync(p => p.DbObjectId == entity.DbObjectId, entity);
            }

            entityTask = context.Entities.Find(p => p.EntityId == existingEntityId).SingleOrDefaultAsync();
            entity = entityTask.Result;
            if (entityTask != null)
            {
                afterReplacement = entity.Name;
            }

            Assert.AreNotEqual(beforeReplacement, afterReplacement); // Test replacement worked properly or not
        }

        [TestMethod]
        public void TestUpdate()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            int existingEntityID = 96;
            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());
            UpdateDefinition<EntityMongoDao> entityUpdateDefinition = Builders<EntityMongoDao>.Update.Set<string>(a => a.UniqueKey, Convert.ToString(repo.GetRandomNumber(1, 100)));
            Task<EntityMongoDao> replacementTask = context.Entities.FindOneAndUpdateAsync(a => a.EntityId == existingEntityID, entityUpdateDefinition);
            Task.WaitAll(replacementTask);

            EntityMongoDao replacementResult = replacementTask.Result;
            Debug.WriteLine(string.Concat(replacementResult.TypeName, ", ", replacementResult.EntityId));

            Assert.AreNotEqual(0, replacementResult.EntityId); // Test update worked properly or not
        }

        [TestMethod]
        public void Seed()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());

            List<EntityMongoDao> entites = new List<EntityMongoDao>();
            List<AttributeDataMongoDao> attrDao = new List<AttributeDataMongoDao>();
            attrDao.Add(new AttributeDataMongoDao { ID = 1, Caption = "Fiscal Year", Value = 1, ValueCaption = "2016" });
            attrDao.Add(new AttributeDataMongoDao { ID = 2, Caption = "Country", Value = 2, ValueCaption = "India" });

            EntityMongoDao newEntity = new EntityMongoDao()
            {
                DbObjectId = MongoDB.Bson.ObjectId.GenerateNewId(), //test to get new objectid
                EntityId = repo.GetRandomNumber(1, 100),
                Name = repo.GenerateRandomEntityName(),
                CreationDate = DateTime.Now,
                AttributeData = attrDao,
                TypeName = "Plan",
                TypeId = 1
            };
            entites.Add(newEntity);

            newEntity = new EntityMongoDao()
            {
                DbObjectId = MongoDB.Bson.ObjectId.GenerateNewId(),
                EntityId = repo.GetRandomNumber(1, 100),
                Name = repo.GenerateRandomEntityName(),
                CreationDate = DateTime.Now,
                AttributeData = attrDao,
                TypeName = "Plan",
                TypeId = 1
            };
            entites.Add(newEntity);

            Task addManyEntitiesTask = context.Entities.InsertManyAsync(entites);
            Task.WaitAll(addManyEntitiesTask);

            Assert.IsNotNull(entites[0].DbObjectId); // Test Seed worked properly or not
        }

        [TestMethod]
        public void MetadataSetup()
        {
            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());

            InitializeAllDevelopmentInstances(); // initialize the persistance layer and nhibernate engine 

            Guid systemSession = DevelopmentManagerFactory.GetSystemSession();
            IDevelopmentManager developmentManager = DevelopmentManagerFactory.GetDevelopmentManager(systemSession);

          
            MetadataVersionMongoDao newEntity = new MetadataVersionMongoDao()
            {
                VersionId = 1,
                VersionName = "Version1",
                CreatedDate = DateTime.Now,
                EntityTypes = developmentManager.CommonManager.GetObject<EntityTypeMongoDao>(),
                EntityTypeHierarchy = developmentManager.CommonManager.GetObject<EntityTypeHierarchyMongoDao>(),
                EntityTypeAttributeRelation = developmentManager.CommonManager.GetObject<EntityTypeAttributeRelationMongoDao>(),
                EntityFeatures = developmentManager.CommonManager.GetObject<EntitytypeFeatureMongoDao>(),
                Attributes = developmentManager.CommonManager.GetObject<AttributeMongoDao>(),
                Features = developmentManager.CommonManager.GetObject<FeatureMongoDao>(),
                Modules = developmentManager.CommonManager.GetObject<ModuleMongoDao>(),
                Options = developmentManager.CommonManager.GetObject<OptionMongoDao>(),
                TreeLevels = developmentManager.CommonManager.GetObject<TreeLevelMongoDao>(),
                TreeNodes = developmentManager.CommonManager.GetObject<TreeNodeMongoDao>()

            };
            Task insertionTask = context.MetadataVersion("version1").InsertOneAsync(newEntity);
            Task.WaitAll(insertionTask);


        }

        [TestMethod]
        public void MetadataVersionDetails()
        {
            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            Task<MetadataVersionMongoDao> versionDetailTask = context.MetadataVersion("version1").Find(a => a.VersionId == 1).SingleOrDefaultAsync();
            Task.WaitAll(versionDetailTask);
            MetadataVersionMongoDao planEntites = versionDetailTask.Result;

        }

    }
}
