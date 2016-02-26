using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                attrDao.Add(new AttributeDataMongoDao { ID = attr.ID, Caption = attr.Caption, Value = attr.Value, ValueCaption = attr.ValueCaption });
            }
            return new Entity(
                loadEntityDbModel.EntityId,
                attrDao,
                loadEntityDbModel.Name,
                  loadEntityDbModel.UniqueKey,
                   loadEntityDbModel.TypeName,
                    loadEntityDbModel.TypeId,
                loadEntityDbModel.CreationDate);
        }

        public static IEnumerable<Entity> ConvertToDomains(this IEnumerable<EntityMongoDao> loadtestDbModels)
        {
            foreach (EntityMongoDao db in loadtestDbModels)
            {
                yield return db.ConvertToDomain();
            }
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
