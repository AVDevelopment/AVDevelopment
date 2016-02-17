using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AV.Development.Core;
using AV.Development.Core.Interface;
using AV.Development.Core.Managers;
using AV.Development.Core.Mongo;
using Development.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace AV.Development.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
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

            // Initialize our database provider.
            Setup.Initialize();

            List<messagescopy> dragons1 = new List<messagescopy>();
            dragons1 = DragonManager.GetPaginationAllEnron(1, 10);

            Expression<Func<messagescopy, bool>> filter = child => child.mailbox == "bass-e";
            var dt = DragonManager.FindSingleMessage(filter);
            var dt1 = DragonManager.FindgroupMessage(filter);

            //List<messagescopy> dragons12 = DragonManager.GetAllEnron().Where(a => a.mailbox == "bass-e").ToList();
            ////get all dragons
            //dragons1 = DragonManager.GetAll();
            //// Search for dragons.
            //List<Dragon> dragons2 = DragonManager.Find("Evil Legendary", 1, 1);
            //// Search for dragons.
            //List<Dragon> dragons = DragonManager.Find("Evil Legendary");
            ////List<countryshortcodes> dragons1 = DragonManager.GetAllCountry().Where(a => a.name.ToLower().StartsWith("e")).ToList();


            //UpdateSearcEngineRecursiveLoop(1, 10000);      // Demonstartion for search index updation;

            Setup.Close();

            Assert.AreNotEqual(0, dragons1.Count);


        }

        public int UpdateSearcEngineRecursiveLoop(int pageNumber, int PageSize)
        {

            List<messagescopy> dragons1 = new List<messagescopy>();
            dragons1 = DragonManager.GetPaginationAllEnron(pageNumber, PageSize);
            foreach (messagescopy mes in dragons1)
            {
                string messaBody = mes.body;
            }
            if (dragons1.Count == 0)
                return 0;
            else if (dragons1.Count >= 0)
                return UpdateSearcEngineRecursiveLoop(pageNumber + 1, PageSize);
            else return 0;


        }



    }
}
