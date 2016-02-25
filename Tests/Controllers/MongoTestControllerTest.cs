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
            DevelopmentManagerFactory.InitializeSystem();

        }


        [TestMethod]
        public void MONGODBTEST()
        {

            InitializeAllDevelopmentInstances(); // initialize the persistance layer and nhibernate engine 

            // Arrange
            Guid systemSession = DevelopmentManagerFactory.GetSystemSession();
            IDevelopmentManager developmentManager = DevelopmentManagerFactory.GetDevelopmentManager(systemSession);


            IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());
            IList<EntityMongoDao> loadtestsInPeriod = repo.GetEntities(1, 100);
            List<EntityMongoDao> toBeInserted = new List<EntityMongoDao>();
            List<EntityMongoDao> toBeUpdated = new List<EntityMongoDao>();
            EntityMongoDao ltNewOne = new EntityMongoDao { EntityId = 1, Name = "Entity A", CreationDate = DateTime.Now };
            EntityMongoDao ltNewTwo = new EntityMongoDao { EntityId = 2, Name = "Entity B", CreationDate = DateTime.Now };
            toBeInserted.Add(ltNewOne);
            toBeInserted.Add(ltNewTwo);
            EntityMongoDao ltUpdOne = new EntityMongoDao { EntityId = 1, Name = "Test Entity A", CreationDate = DateTime.Now.AddDays(1) };
            toBeUpdated.Add(ltUpdOne);
            repo.AddOrUpdateLoadEntites(toBeInserted, toBeUpdated);

        }

        [TestMethod]
        public void TestSelectAllNoFilter()
        {

            InitializeAllDevelopmentInstances(); // initialize the persistance layer and nhibernate engine 

            MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
            Task<List<EntityMongoDao>> dbEngineersTask = context.Entities.Find(x => true).ToListAsync();
            Task.WaitAll(dbEngineersTask);
            List<EntityMongoDao> dbEngineers = dbEngineersTask.Result;

            foreach (EntityMongoDao eng in dbEngineers)
            {
                Debug.WriteLine(eng.Name);
            }
        }


    }
}
