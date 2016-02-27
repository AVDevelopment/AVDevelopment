
/*
 * Copyright (c) 2016 MARKETROBO-AVD
 * Distributed under the MIT license - http://opensource.org/licenses/MIT
 *
 * Written with CSharpDriver-2.2.3
 * Documentation: http://api.mongodb.org/csharp/
 * A C# class connecting to a MongoDB database given a MongoDB Connection URI.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Repositories.Interface;
using AV.Development.Dal.Base;
using MongoDB.Driver.Builders;
using AV.Development.Dal.MongoDB.Domain;
using System.Threading.Tasks;
using AV.Development.Dal.Metadata.Model;

namespace AV.Development.Dal.MongoDB.Repositories
{

    public class ConfigurationRepository : GenericRepository, IConfigurationRepository
    {
        public ConfigurationRepository(IMongoConnectionStringRepository connectionStringRepository)
            : base(connectionStringRepository)
        { }


        ///<summary>
        ///Get all entites without pagination.
        ///</summary>
        public IList<EntityMongoDao> GetEntities()
        {

            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            List<EntityMongoDao> mongoDbLoadEntitesInSearchPeriod = context.Entities.Find(x => true)
                .ToList();

            return mongoDbLoadEntitesInSearchPeriod;
        }

        ///<summary>
        ///Get entites with pagination.
        ///</summary>
        public IList<EntityMongoDao> GetEntities(int pageNo, int pageSize)
        {
            int skipCount = (pageNo - 1) * 20;

            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            List<EntityMongoDao> mongoDbLoadEntitesInSearchPeriod = context.Entities.Find(x => true).Skip(skipCount).Limit(pageSize)
                .ToList();

            return mongoDbLoadEntitesInSearchPeriod;
        }


        ///<summary>
        ///Get entites by order caluse (Asc: {TypeName}, Desc:{CreationDate}).
        ///</summary>
        public IList<Entity> GetEntities(int pageNo, int pageSize, bool sortby)
        {
            int skipCount = (pageNo - 1) * pageSize;

            var builder = Builders<EntityMongoDao>.Sort;
            var sort = builder.Ascending(x => x.EntityId).Descending(x => x.CreationDate);

            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            List<EntityMongoDao> mongoDbLoadEntites = context.Entities.Find(x => true).Sort(sort).Skip(skipCount).Limit(pageSize)
                .ToList();

            IList<Entity> entitesLst = mongoDbLoadEntites.ConvertToDomains().ToList();


            return entitesLst;
        }

        ///<summary>
        ///Get all entites between creation dates.
        ///</summary>
        public IList<EntityMongoDao> GetEntitiesForTimePeriod(DateTime searchStartDateUtc, DateTime searchEndDateUtc)
        {
            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);

            var dateQueryBuilder = Builders<EntityMongoDao>.Filter;
            var startDateBeforeSearchStartFilter = dateQueryBuilder.Lte<DateTime>(l => l.CreationDate, searchStartDateUtc);
            var endDateAfterSearchStartFilter = dateQueryBuilder.Gte<DateTime>(l => l.CreationDate, searchStartDateUtc);
            var firstPartialDateQuery = dateQueryBuilder.And(new List<FilterDefinition<EntityMongoDao>>() { startDateBeforeSearchStartFilter, endDateAfterSearchStartFilter });

            var startDateBeforeSearchEndFilter = dateQueryBuilder.Lte<DateTime>(l => l.CreationDate, searchEndDateUtc);
            var endDateAfterSearchEndFilter = dateQueryBuilder.Gte<DateTime>(l => l.CreationDate, searchEndDateUtc);
            var secondPartialDateQuery = dateQueryBuilder.And(new List<FilterDefinition<EntityMongoDao>>() { startDateBeforeSearchEndFilter, endDateAfterSearchEndFilter });

            var thirdPartialDateQuery = dateQueryBuilder.And(new List<FilterDefinition<EntityMongoDao>>() { startDateBeforeSearchStartFilter, endDateAfterSearchEndFilter });

            var startDateAfterSearchStartFilter = dateQueryBuilder.Gte<DateTime>(l => l.CreationDate, searchStartDateUtc);
            var endDateBeforeSearchEndFilter = dateQueryBuilder.Lte<DateTime>(l => l.CreationDate, searchEndDateUtc);
            var fourthPartialQuery = dateQueryBuilder.And(new List<FilterDefinition<EntityMongoDao>>() { startDateAfterSearchStartFilter, endDateBeforeSearchEndFilter });

            var ultimateQuery = dateQueryBuilder.Or(new List<FilterDefinition<EntityMongoDao>>() { firstPartialDateQuery, secondPartialDateQuery, thirdPartialDateQuery, fourthPartialQuery });

            List<EntityMongoDao> mongoDbLoadtestsInSearchPeriod = context.Entities.Find(ultimateQuery)
                .ToList();

            return mongoDbLoadtestsInSearchPeriod;
        }

        ///<summary>
        ///Add or update entities
        ///</summary>
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
                    EntityMongoDao loadentityInDb = loadentityInDbQuery.FirstOrDefault();
                    if (loadentityInDb != null)
                    {
                        loadentityInDb.Name = toBeUpdated.Name;
                        loadentityInDb.CreationDate = toBeUpdated.CreationDate;
                        context.Entities.FindOneAndReplace<EntityMongoDao>(lt => lt.DbObjectId == loadentityInDb.DbObjectId, loadentityInDb);
                    }
                }
            }
        }


        ///<summary>
        ///Delete entity by id
        ///</summary>
        public void DeleteById(int id)
        {
            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            context.Entities.FindOneAndDelete<EntityMongoDao>(lt => lt.EntityId == id);
        }


        ///<summary>
        ///Get metadata version details.
        ///</summary>
        public MetadataVersionMongoDao MetadataVersion(string versionCollectionName)
        {

            MarketRoboContext context = MarketRoboContext.Create(base.ConnectionStringRepository);
            MetadataVersionMongoDao mongoDbVersion = context.MetadataVersion(versionCollectionName).Find(x => true).SingleOrDefault();
            return mongoDbVersion;
        }


        ///<summary>
        ///GenerateRandomEntityName
        ///</summary>
        public string GenerateRandomEntityName()
        {
            return this.GenRandomLastName() + " " + this.GenRandomFirstName();
        }

        /// <summary>
        /// Get MetaData Objects
        /// </summary>
        public List<T> GetObject<T>(string collectionName)
        {
            List<T> obj = null;
            try
            {

                MarketRoboContext context = MarketRoboContext.Create(new WebConfigConnectionStringRepository());
                Task<MetadataVersionMongoDao> versionDetailTask = context.MetadataVersion("version1").Find(x => true).SingleOrDefaultAsync();
                Task.WaitAll(versionDetailTask);
                MetadataVersionMongoDao versionDetail = versionDetailTask.Result;

                if (typeof(T).Name == "EntityTypeMongoDao")
                {
                    obj = versionDetail.EntityTypes.Cast<T>().ToList();

                }
            }
            catch
            {

            }

            return obj;
        }


        #region testUtilityMethods

        public static Random rnd = new Random();
        public string GenRandomLastName()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Smith");
            lst.Add("Johnson");
            lst.Add("Williams");
            lst.Add("Jones");
            lst.Add("Brown");
            lst.Add("Davis");
            lst.Add("Miller");
            lst.Add("Wilson");
            lst.Add("Moore");
            lst.Add("Taylor");
            lst.Add("Anderson");
            lst.Add("Thomas");
            lst.Add("Jackson");
            lst.Add("White");
            lst.Add("Harris");
            lst.Add("Martin");
            lst.Add("Thompson");
            lst.Add("Garcia");
            lst.Add("Martinez");
            lst.Add("Robinson");
            lst.Add("Clark");
            lst.Add("Rodriguez");
            lst.Add("Lewis");
            lst.Add("Lee");
            lst.Add("Walker");
            lst.Add("Hall");
            lst.Add("Allen");
            lst.Add("Young");
            lst.Add("Hernandez");
            lst.Add("King");
            lst.Add("Wright");
            lst.Add("Lopez");
            lst.Add("Hill");
            lst.Add("Scott");
            lst.Add("Green");
            lst.Add("Adams");
            lst.Add("Baker");
            lst.Add("Gonzalez");
            lst.Add("Nelson");
            lst.Add("Carter");
            lst.Add("Mitchell");
            lst.Add("Perez");
            lst.Add("Roberts");
            lst.Add("Turner");
            lst.Add("Phillips");
            lst.Add("Campbell");
            lst.Add("Parker");
            lst.Add("Evans");
            lst.Add("Edwards");
            lst.Add("Collins");
            lst.Add("Stewart");
            lst.Add("Sanchez");
            lst.Add("Morris");
            lst.Add("Rogers");
            lst.Add("Reed");
            lst.Add("Cook");
            lst.Add("Morgan");
            lst.Add("Bell");
            lst.Add("Murphy");
            lst.Add("Bailey");
            lst.Add("Rivera");
            lst.Add("Cooper");
            lst.Add("Richardson");
            lst.Add("Cox");
            lst.Add("Howard");
            lst.Add("Ward");
            lst.Add("Torres");
            lst.Add("Peterson");
            lst.Add("Gray");
            lst.Add("Ramirez");
            lst.Add("James");
            lst.Add("Watson");
            lst.Add("Brooks");
            lst.Add("Kelly");
            lst.Add("Sanders");
            lst.Add("Price");
            lst.Add("Bennett");
            lst.Add("Wood");
            lst.Add("Barnes");
            lst.Add("Ross");
            lst.Add("Henderson");
            lst.Add("Coleman");
            lst.Add("Jenkins");
            lst.Add("Perry");
            lst.Add("Powell");
            lst.Add("Long");
            lst.Add("Patterson");
            lst.Add("Hughes");
            lst.Add("Flores");
            lst.Add("Washington");
            lst.Add("Butler");
            lst.Add("Simmons");
            lst.Add("Foster");
            lst.Add("Gonzales");
            lst.Add("Bryant");
            lst.Add("Alexander");
            lst.Add("Russell");
            lst.Add("Griffin");
            lst.Add("Diaz");
            lst.Add("Hayes");

            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;
        }
        public string GenRandomFirstName()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Aiden");
            lst.Add("Jackson");
            lst.Add("Mason");
            lst.Add("Liam");
            lst.Add("Jacob");
            lst.Add("Jayden");
            lst.Add("Ethan");
            lst.Add("Noah");
            lst.Add("Lucas");
            lst.Add("Logan");
            lst.Add("Caleb");
            lst.Add("Caden");
            lst.Add("Jack");
            lst.Add("Ryan");
            lst.Add("Connor");
            lst.Add("Michael");
            lst.Add("Elijah");
            lst.Add("Brayden");
            lst.Add("Benjamin");
            lst.Add("Nicholas");
            lst.Add("Alexander");
            lst.Add("William");
            lst.Add("Matthew");
            lst.Add("James");
            lst.Add("Landon");
            lst.Add("Nathan");
            lst.Add("Dylan");
            lst.Add("Evan");
            lst.Add("Luke");
            lst.Add("Andrew");
            lst.Add("Gabriel");
            lst.Add("Gavin");
            lst.Add("Joshua");
            lst.Add("Owen");
            lst.Add("Daniel");
            lst.Add("Carter");
            lst.Add("Tyler");
            lst.Add("Cameron");
            lst.Add("Christian");
            lst.Add("Wyatt");
            lst.Add("Henry");
            lst.Add("Eli");
            lst.Add("Joseph");
            lst.Add("Max");
            lst.Add("Isaac");
            lst.Add("Samuel");
            lst.Add("Anthony");
            lst.Add("Grayson");
            lst.Add("Zachary");
            lst.Add("David");
            lst.Add("Christopher");
            lst.Add("John");
            lst.Add("Isaiah");
            lst.Add("Levi");
            lst.Add("Jonathan");
            lst.Add("Oliver");
            lst.Add("Chase");
            lst.Add("Cooper");
            lst.Add("Tristan");
            lst.Add("Colton");
            lst.Add("Austin");
            lst.Add("Colin");
            lst.Add("Charlie");
            lst.Add("Dominic");
            lst.Add("Parker");
            lst.Add("Hunter");
            lst.Add("Thomas");
            lst.Add("Alex");
            lst.Add("Ian");
            lst.Add("Jordan");
            lst.Add("Cole");
            lst.Add("Julian");
            lst.Add("Aaron");
            lst.Add("Carson");
            lst.Add("Miles");
            lst.Add("Blake");
            lst.Add("Brody");
            lst.Add("Adam");
            lst.Add("Sebastian");
            lst.Add("Adrian");
            lst.Add("Nolan");
            lst.Add("Sean");
            lst.Add("Riley");
            lst.Add("Bentley");
            lst.Add("Xavier");
            lst.Add("Hayden");
            lst.Add("Jeremiah");
            lst.Add("Jason");
            lst.Add("Jake");
            lst.Add("Asher");
            lst.Add("Micah");
            lst.Add("Jace");
            lst.Add("Brandon");
            lst.Add("Josiah");
            lst.Add("Hudson");
            lst.Add("Nathaniel");
            lst.Add("Bryson");
            lst.Add("Ryder");
            lst.Add("Justin");
            lst.Add("Bryce");

            //—————female

            lst.Add("Sophia");
            lst.Add("Emma");
            lst.Add("Isabella");
            lst.Add("Olivia");
            lst.Add("Ava");
            lst.Add("Lily");
            lst.Add("Chloe");
            lst.Add("Madison");
            lst.Add("Emily");
            lst.Add("Abigail");
            lst.Add("Addison");
            lst.Add("Mia");
            lst.Add("Madelyn");
            lst.Add("Ella");
            lst.Add("Hailey");
            lst.Add("Kaylee");
            lst.Add("Avery");
            lst.Add("Kaitlyn");
            lst.Add("Riley");
            lst.Add("Aubrey");
            lst.Add("Brooklyn");
            lst.Add("Peyton");
            lst.Add("Layla");
            lst.Add("Hannah");
            lst.Add("Charlotte");
            lst.Add("Bella");
            lst.Add("Natalie");
            lst.Add("Sarah");
            lst.Add("Grace");
            lst.Add("Amelia");
            lst.Add("Kylie");
            lst.Add("Arianna");
            lst.Add("Anna");
            lst.Add("Elizabeth");
            lst.Add("Sophie");
            lst.Add("Claire");
            lst.Add("Lila");
            lst.Add("Aaliyah");
            lst.Add("Gabriella");
            lst.Add("Elise");
            lst.Add("Lillian");
            lst.Add("Samantha");
            lst.Add("Makayla");
            lst.Add("Audrey");
            lst.Add("Alyssa");
            lst.Add("Ellie");
            lst.Add("Alexis");
            lst.Add("Isabelle");
            lst.Add("Savannah");
            lst.Add("Evelyn");
            lst.Add("Leah");
            lst.Add("Keira");
            lst.Add("Allison");
            lst.Add("Maya");
            lst.Add("Lucy");
            lst.Add("Sydney");
            lst.Add("Taylor");
            lst.Add("Molly");
            lst.Add("Lauren");
            lst.Add("Harper");
            lst.Add("Scarlett");
            lst.Add("Brianna");
            lst.Add("Victoria");
            lst.Add("Liliana");
            lst.Add("Aria");
            lst.Add("Kayla");
            lst.Add("Annabelle");
            lst.Add("Gianna");
            lst.Add("Kennedy");
            lst.Add("Stella");
            lst.Add("Reagan");
            lst.Add("Julia");
            lst.Add("Bailey");
            lst.Add("Alexandra");
            lst.Add("Jordyn");
            lst.Add("Nora");
            lst.Add("Carolin");
            lst.Add("Mackenzie");
            lst.Add("Jasmine");
            lst.Add("Jocelyn");
            lst.Add("Kendall");
            lst.Add("Morgan");
            lst.Add("Nevaeh");
            lst.Add("Maria");
            lst.Add("Eva");
            lst.Add("Juliana");
            lst.Add("Abby");
            lst.Add("Alexa");
            lst.Add("Summer");
            lst.Add("Brooke");
            lst.Add("Penelope");
            lst.Add("Violet");
            lst.Add("Kate");
            lst.Add("Hadley");
            lst.Add("Ashlyn");
            lst.Add("Sadie");
            lst.Add("Paige");
            lst.Add("Katherine");
            lst.Add("Sienna");
            lst.Add("Piper");

            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;
        }
        public string GenRandomEntityTypeName()
        {
            List<string> lst = new List<string>();
            string str = string.Empty;
            lst.Add("Plan");
            lst.Add("Milestone");
            lst.Add("Task");
            lst.Add("Asset");
            lst.Add("CostCenter");
            lst.Add("Objectives");
            lst.Add("Calander");
            str = lst.OrderBy(xx => rnd.Next()).First();
            return str;
        }
        public int GenRandomEntityType()
        {
            List<int> lst = new List<int>();
            int nmbr = 1;
            lst.Add(1);
            lst.Add(2);
            lst.Add(3);
            lst.Add(4);
            lst.Add(5);
            lst.Add(6);
            lst.Add(7);
            nmbr = lst.OrderBy(xx => rnd.Next()).First();
            return nmbr;
        }

        ///<summary>
        ///GetRandomNumber
        ///</summary>
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        #endregion

    }
}
