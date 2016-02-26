using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.MongoDB.DatabaseObjects;

namespace AV.Development.Dal.MongoDB.Domain
{
    public class Entity
    {
        private Entity() { }

        public int EntityId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string UniqueKey { get; set; }
        public List<AttributeDataMongoDao> AttributeData { get; set; }
        public string TypeName { get; set; }
        public int TypeId { get; set; }


        public Entity(int entityId, List<AttributeDataMongoDao> parameters, string name, string uniqueKey, string typeName
            , int typeid, DateTime createdDate)
        {
            AssignParameters(parameters, entityId, name, uniqueKey, typeName, typeid, createdDate);
        }

        private void AssignParameters(List<AttributeDataMongoDao> parameters, int entityId, string name, string uniqueKey, string typeName, int typeid, DateTime createdDate)
        {
            EntityId = entityId; Name = name; CreationDate = createdDate;
            TypeId = typeid; TypeName = typeName; AttributeData = parameters;
        }

        public void Update(List<AttributeDataMongoDao> parameters, int entityId, string name, string uniqueKey
            , string typeName, int typeid, DateTime createdDate)
        {
            AssignParameters(parameters, entityId, name, uniqueKey, typeName, typeid, createdDate);
        }

      
    }
}
