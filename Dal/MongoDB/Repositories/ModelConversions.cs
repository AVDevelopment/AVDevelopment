using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.Metadata.Model;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Domain;
using MongoDB.Bson;

namespace AV.Development.Dal.MongoDB.Repositories
{
    public static class ModelConversions
    {
        public static Entity ConvertToDomain(this EntityMongoDao loadEntityDbModel)
        {
            List<AttributeDataMongoDao> attrDao = new List<AttributeDataMongoDao>();
            foreach (var attr in loadEntityDbModel.AttributeData)
            {
                attrDao.Add(new AttributeDataMongoDao { AttributeID = attr.AttributeID, Caption = attr.Caption, Value = attr.Value, ValueCaption = attr.ValueCaption });
            }
            return new Entity(
                loadEntityDbModel.EntityId,
                attrDao,
                loadEntityDbModel.Name,
                loadEntityDbModel.UniqueKey,
                loadEntityDbModel.TypeName,
                loadEntityDbModel.TypeId,
                loadEntityDbModel.CreationDate
                );
        }

        public static EntityTypeDao ConvertToEntityTypeDomain(this EntityTypeMongoDao loadTypeDbModel)
        {

            return new EntityTypeDao
            {
                Id = loadTypeDbModel.EntityTypeId,
                Caption = loadTypeDbModel.Caption,
                ColorCode = loadTypeDbModel.ColorCode,
                Category = loadTypeDbModel.Category,
                Description = loadTypeDbModel.Description,
                ModuleID = loadTypeDbModel.ModuleID,
                IsAssociate = loadTypeDbModel.IsAssociate,
                IsRootLevel = loadTypeDbModel.IsRootLevel,
                ShortDescription = loadTypeDbModel.ShortDescription
            };
        }

        public static FeatureDao ConvertToFeatureDomain(this FeatureMongoDao loadFeatureDbModel)
        {

            return new FeatureDao
            {
                Id = loadFeatureDbModel.FeatureId,
                Caption = loadFeatureDbModel.Caption,
                Description = loadFeatureDbModel.Description,
                ModuleID = loadFeatureDbModel.ModuleID,
                IsEnable = loadFeatureDbModel.IsEnable,
                IsTopNavigation = loadFeatureDbModel.IsTopNavigation

            };
        }

        public static IEnumerable<Entity> ConvertToDomains(this IEnumerable<EntityMongoDao> loadtestDbModels)
        {
            foreach (EntityMongoDao db in loadtestDbModels)
            {
                yield return db.ConvertToDomain();
            }
        }

        public static List<EntityTypeDao> ConvertToDomains(this List<EntityTypeMongoDao> loadtestDbModels)
        {
            List<EntityTypeDao> entitytypeLst = new List<EntityTypeDao>();
            foreach (EntityTypeMongoDao db in loadtestDbModels)
            {
                entitytypeLst.Add(db.ConvertToEntityTypeDomain());
            }
            return entitytypeLst;
        }

      
        public static EntityMongoDao PrepareForInsertion(this Entity domain)
        {
            EntityMongoDao ltDb = new EntityMongoDao();
            ltDb.EntityId = domain.EntityId;
            ltDb.Name = domain.Name;
            ltDb.CreationDate = domain.CreationDate;
            ltDb.TypeName = domain.TypeName;
            ltDb.TypeId = domain.TypeId;
            ltDb.UniqueKey = domain.UniqueKey;
            ltDb.AttributeData = domain.AttributeData;
            return ltDb;
        }

        public static IEnumerable<EntityMongoDao> PrepareAllForInsertion(this IEnumerable<Entity> domains)
        {
            foreach (Entity domain in domains)
            {
                yield return domain.PrepareForInsertion();
            }
        }

    }

}
