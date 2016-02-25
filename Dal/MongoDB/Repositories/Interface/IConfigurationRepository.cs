using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Dal.MongoDB.DatabaseObjects;

namespace AV.Development.Dal.MongoDB.Repositories.Interface
{
    public interface IConfigurationRepository
    {
        IList<EntityMongoDao> GetEntities(int pageNo, int pageSize);
        void AddOrUpdateLoadEntites(List<EntityMongoDao> ToBeInserted = null, List<EntityMongoDao> ToBeUpdated = null);
        void DeleteById(int id);
    }


}
