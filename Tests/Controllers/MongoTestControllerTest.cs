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
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;



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
            AttributeDataMongoDao attr1 = new AttributeDataMongoDao { DbObjectId = MongoDB.Bson.ObjectId.GenerateNewId(), AttributeID = 1, Caption = "Fiscal Year", Value = "1", ValueCaption = "2016" };
            AttributeDataMongoDao attr2 = new AttributeDataMongoDao { DbObjectId = MongoDB.Bson.ObjectId.GenerateNewId(), AttributeID = 2, Caption = "Country", Value = "2", ValueCaption = "India" };
            attrDao.Add(attr1);
            attrDao.Add(attr2);

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
        public void TestWithMatchGroup()
        {

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            Task<List<EntityMongoDao>> dbEntitiesTask = context.Entities.Find(x => true).ToListAsync();
            Task.WaitAll(dbEntitiesTask);

            var collection = context.MetadataVersion("version1");
            var aggregate = collection.Aggregate()
                .Match(new BsonDocument { { "VersionId", 1 } })
                .Unwind(x => x.EntityTypes)
                .Match(new BsonDocument { { "EntityTypes.EntityTypeId", new BsonDocument { { "$gt", 138 } } } })
                .Group(new BsonDocument { { "_id", "$_id" }, { "list", new BsonDocument { { "$push", "$EntityTypes" } } } });
            var results = aggregate.ToList();

            var aggregate1 = collection.Aggregate()
                .Match(new BsonDocument { { "VersionId", 1 } })
                .Unwind(x => x.EntityTypes)
                .Match(new BsonDocument { { "EntityTypes.EntityTypeId", new BsonDocument { { "$gt", 1 } } } })
                .Group(new BsonDocument { { "_id", "$_id" }, { "list", new BsonDocument { { "$push", "$EntityTypes" } } } }).FirstOrDefault();

            BsonValue dimVal1 = aggregate1["list"];
            List<EntityTypeMongoDao> d = BsonSerializer.Deserialize<List<EntityTypeMongoDao>>(dimVal1.ToJson());

            var aggregate2 = context.MetadataVersion("version1").Aggregate()
            .Match(new BsonDocument { { "VersionId", 1 } })
            .Unwind(x => x.EntityTypeAttributeRelation)
            .Match(new BsonDocument { { "EntityTypeAttributeRelation.EntityTypeID", new BsonDocument { { "$eq", 138 } } } })
            .Group(new BsonDocument { { "_id", "$_id" }, { "list", new BsonDocument { { "$push", "$EntityTypeAttributeRelation" } } } }).FirstOrDefault();
            BsonValue dimVal = aggregate2["list"];
            var result2 = BsonSerializer.Deserialize<List<EntityTypeAttributeRelationMongoDao>>(dimVal.ToJson());



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


            var findFluentCol = context.MetadataVersion("version1").Find(f => f.VersionId == 1 && f.EntityTypeAttributeRelation.Any(fb => fb.EntityTypeID == 63)).ToList();
            var findFluent = context.MetadataVersion("version1").Find(Builders<MetadataVersionMongoDao>.Filter.ElemMatch(foo => foo.EntityTypeAttributeRelation, foobar => foobar.EntityTypeID == 63)).ToList();
            var condition = Builders<MetadataVersionMongoDao>.Filter.Eq(p => p.VersionId, 1);
            var fields = Builders<MetadataVersionMongoDao>.Projection.Include(p => p.EntityTypeAttributeRelation);
            var result = context.MetadataVersion("version1").Find(condition).Project<EntityTypeAttributeRelationMongoDao>(fields).ToList();

            var dateQueryBuilder = Builders<EntityTypeAttributeRelationMongoDao>.Filter;
            var startDateBeforeSearchStartFilter = dateQueryBuilder.Eq<int>(l => l.EntityTypeID, 1);
            var dateQueryBuilder1 = Builders<MetadataVersionMongoDao>.Filter;
            var startDateBeforeSearchStartFilter1 = dateQueryBuilder1.ElemMatch(p => p.EntityTypeAttributeRelation, startDateBeforeSearchStartFilter);
            var mongoDbLoadtestsInSearchPeriod = context.MetadataVersion("version1").Find(startDateBeforeSearchStartFilter1)
            .ToList();

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
            attrDao.Add(new AttributeDataMongoDao { AttributeID = 1, Caption = "Fiscal Year", Value = "1", ValueCaption = "2016" });
            attrDao.Add(new AttributeDataMongoDao { AttributeID = 2, Caption = "Country", Value = "2", ValueCaption = "India" });
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
            attrDao.Add(new AttributeDataMongoDao { AttributeID = 1, Caption = "Fiscal Year", Value = "1", ValueCaption = "2016" });
            attrDao.Add(new AttributeDataMongoDao { AttributeID = 2, Caption = "Country", Value = "2", ValueCaption = "India" });

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

        [TestMethod]
        public void SeacrhWithSortPropertyName()
        {
            int pageNo = 1, pageSize = 20;
            int skipCount = (pageNo - 1) * pageSize;
            string propertyInfo = GetPropertyInfo(new EntityMongoDao(), u => u.EntityId).Name;

            var builder = Builders<EntityMongoDao>.Sort;
            var sort = builder.Ascending(propertyInfo);

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            //List<EntityMongoDao> mongoDbLoadEntites = context.Entities.Find(x => true).Sort(sort).SortBy(x => x.Name).ThenByDescending(x => x.EntityId).ThenByDescending(x => x.CreationDate).Skip(skipCount).Limit(pageSize)
            //    .ToList();
            List<EntityMongoDao> mongoDbLoadEntites = context.Entities.Find(x => true).Sort(sort).Skip(skipCount).Limit(pageSize)
               .ToList();
        }


        public PropertyInfo GetPropertyInfo<TSource, TProperty>(
    TSource source,
    Expression<Func<TSource, TProperty>> propertyLambda)
        {

            PropertyInfo propInfo = null;
            try
            {

                Type type = typeof(TSource);
                MemberExpression member = propertyLambda.Body as MemberExpression;
                if (member == null)
                {
                    //throw new ArgumentException(string.Format(
                    //    "Expression '{0}' refers to a method, not a property.",
                    //    propertyLambda.ToString()));
                }


                propInfo = member.Member as PropertyInfo;
                if (propInfo == null)
                {
                    //throw new ArgumentException(string.Format(
                    //    "Expression '{0}' refers to a field, not a property.",
                    //    propertyLambda.ToString()));
                }

                if (type != propInfo.ReflectedType &&
                    !type.IsSubclassOf(propInfo.ReflectedType))
                {
                    //throw new ArgumentException(string.Format(
                    //    "Expresion '{0}' refers to a property that is not from type {1}.",
                    //    propertyLambda.ToString(),
                    //    type));
                }
            }
            catch
            {

            }

            return propInfo;
        }

    }
}
