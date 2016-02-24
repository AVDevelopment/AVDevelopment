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
using MongoDB.Driver;



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


            var MongoDbContext = new MongoDbContext();
            var user = MongoDbContext.Posts.Find(x => x.headers.From == "reservations@marriott.com").FirstOrDefault();

            var user1 = MongoDbContext.Posts.Find(x => true).FirstOrDefault().headers;

            Expression<Func<messagescopy_copy1, bool>> filter = x => true;

            Expression<Func<messagescopy_copy1, bool>> filter1 = child => child.headers.From == "reservations@marriott.com";

            string to = "reservations@marriott.com";

            if (to != null)
            {
                filter = x => x.Id == "4f16fc97d1e2d32371003f02" && x.headers.To.Contains("ebass@enron.com");
            }

            var posts = MongoDbContext.Posts.Find(filter)
                .SortByDescending(x => x.headers.Date)
                .ToList();

            var posts1 = MongoDbContext.Posts.Find(filter1)
               .SortByDescending(x => x.headers.Date)
               .ToList();


            var searchResult = MongoDbContext.Posts.Find(x => x.headers.From.Contains("reservations")).SortByDescending(a => a.Id).ToList();

            var searchResult1 = MongoDbContext.Posts.Find(x => x.headers.To.Contains("ebass@enron.com")).SortByDescending(a => a.Id).ToList();

            var searchResult2 = MongoDbContext.Posts.Find(x => x.body.Contains("PLEASE DO NOT REPLY TO THIS EMAIL")).SortByDescending(a => a.Id).ToList();

            var testResult = user1;

        }

        public int UpdateSearcEngineRecursiveLoop(int pageNumber, int PageSize)
        {

            //List<messagescopy> dragons1 = new List<messagescopy>();
            //dragons1 = DragonManager.GetPaginationAllEnron(pageNumber, PageSize);
            //foreach (messagescopy mes in dragons1)
            //{
            //    string messaBody = mes.body;
            //}
            //if (dragons1.Count == 0)
            //    return 0;
            //else if (dragons1.Count >= 0)
            //    return UpdateSearcEngineRecursiveLoop(pageNumber + 1, PageSize);
            //else return 0;

            return 0;

        }



    }
}
