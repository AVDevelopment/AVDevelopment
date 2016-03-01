using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Domain;

namespace AV.Development.Dal.MongoDB.Repositories.Interface
{
    public interface IConfigurationRepository
    {
        IList<EntityMongoDao> GetEntities();
        IList<EntityMongoDao> GetEntitiesForTimePeriod(DateTime searchStartDateUtc, DateTime searchEndDateUtc);
        IList<EntityMongoDao> GetEntities(int pageNo, int pageSize);
        IList<Entity> GetEntities(int pageNo, int pageSize, bool sortby);
        MetadataVersionMongoDao MetadataVersion(string versionCollectionName);
        List<EntityTypeAttributeRelationMongoDao> GetEntityTypeRelationById(string collectionName, int entityTypeId, int versionID);
        void AddOrUpdateLoadEntites(List<EntityMongoDao> ToBeInserted = null, List<EntityMongoDao> ToBeUpdated = null);
        void DeleteById(int id);
        string GenerateRandomEntityName();
        int GetRandomNumber(int min, int max);
        string GenRandomEntityTypeName();
        int GenRandomEntityType();
    }


}
