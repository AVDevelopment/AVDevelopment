using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using AV.Development.Core.Mongo;
using AV.Development.Dal.Mongo.Context;

namespace AV.Development.Core.Managers
{
    public static class RealmManager
    {
        public static Realm GetByRegion(Realm.RegionType region)
        {
            return DbContext.Current.Single<Realm>(r => r.Region == region);
        }

        public static void Save(Realm realm)
        {
            DbContext.Current.Add(realm);
        }

        #region Helpers

        public static Realm CreateRandom()
        {            
            Realm.RegionType region = (Realm.RegionType)MongoHelperManager.RandomGenerator.Next(1, 5);

            // Load the realm.
            Realm realm = GetByRegion(region);
            if (realm == null)
            {
                // Create the realm if it doesn't exist.
                realm = new Realm(region);
                Save(realm);
            }

            return realm;
        }

        #endregion
    }
}
