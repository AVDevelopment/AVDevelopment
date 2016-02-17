using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using MongoDragons.Repository.Concrete;
using AV.Development.Core.Mongo;
using AV.Development.Dal.Mongo.Context;
using System.Linq.Expressions;

namespace AV.Development.Core.Managers
{
    public static class DragonManager
    {
        #region Names

        private static string[] _firstNames = new string[]
        {
            "White",
            "Black",
            "Light",
            "Dark",
            "Evil",
            "Cunning",
            "Magic",
            "Silver",
            "Golden",
            "Slimy"
        };

        private static string[] _lastNames = new string[]
        {
            "Legendary",
            "Sneaky",
            "Cheating",
            "Stealth",
            "Serpent",
            "Ghost",
            "Chimaera",
            "Hippogryph",
            "Spirit",
            "Skeleton"
        };

        #endregion

        public static List<Dragon> GetAll()
        {
            return DbContext.Current.All<Dragon>().OrderBy(d => d.Name).ToList();
        }

        public static List<messagescopy> GetPaginationAllEnron(int page, int size)
        {
            return DbContext.Current.All<messagescopy>(page, size).ToList();
        }

        public static List<messagescopy> GetAllEnron()
        {
            return DbContext.Current.All<messagescopy>().ToList();
        }

        public static List<countryshortcodes> GetAllCountry()
        {
            return DbContext.Current.All<countryshortcodes>().OrderBy(d => d.name).ToList();
        }

        public static List<Dragon> Find(string keyword)
        {
            List<Dragon> dragons = null;
            if (keyword.Length > 0)
            {
                dragons = DbContext.Current.All<Dragon>().Where(d => d.Name.ToLower().Contains(keyword.ToLower())).OrderBy(d => d.Name).ToList();
            }
            else
            {
                dragons = GetAll();
            }

            return dragons;
        }

        public static messagescopy FindSingleMessage(System.Linq.Expressions.Expression<Func<messagescopy, bool>> expression)
        {
            Expression<Func<messagescopy, bool>> filter = child => child.mailbox == "bass-e";

            var messages = DbContext.Current.Single<messagescopy>(expression); //fast this is

            return messages;
        }

        public static IQueryable<messagescopy> FindgroupMessage(System.Linq.Expressions.Expression<Func<messagescopy, bool>> expression)
        {
            Expression<Func<messagescopy, bool>> filter = child => child.mailbox == "bass-e";

            var messages = DbContext.Current.FilterCondition<messagescopy>(expression); //fast this is

            return messages;
        }

        public static List<messagescopy> FindEnronMessage(string keyword)
        {

            List<messagescopy> messages = null;

            if (keyword.Length > 0)
            {

                messages = DbContext.Current.All<messagescopy>().Where(d => d.mailbox.ToLower().Contains(keyword.ToLower())).OrderBy(d => d.mailbox).ToList();
            }
            else
            {
                messages = GetAllEnron();
            }

            return messages;
        }

        public static List<Dragon> Find(string keyword, int page, int pageSize = 1)
        {
            List<Dragon> dragons = null;

            if (keyword.Length > 0)
            {
                dragons = DbContext.Current.All<Dragon>().Where(d => d.Name.ToLower().Contains(keyword.ToLower())).OrderBy(d => d.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                dragons = GetAll();
            }

            return dragons;
        }

        public static void Save(Dragon dragon)
        {
            DbContext.Current.Add(dragon);
        }

        public static void Delete(Dragon dragon)
        {
            DbContext.Current.Delete<Dragon>(d => d.Id == dragon.Id);
        }

        #region Helpers

        public static Dragon CreateRandom()
        {
            Dragon dragon = new Dragon();
            dragon.Name = MongoHelperManager.CreateRandomName(_firstNames, _lastNames);
            dragon.Age = MongoHelperManager.RandomGenerator.Next(1, 101);
            dragon.Description = "A big dragon.";
            dragon.Gold = MongoHelperManager.RandomGenerator.Next(1, 1001);
            dragon.Weapon = new Breath { Name = "Breath", Description = "A breath attack.", Type = (Breath.BreathType)MongoHelperManager.RandomGenerator.Next(0, 6) };
            dragon.MaxHP = MongoHelperManager.RandomGenerator.Next(10, 21);
            dragon.HP = dragon.MaxHP;
            dragon.Realm = RealmManager.CreateRandom();

            return dragon;
        }

        #endregion
    }
}
