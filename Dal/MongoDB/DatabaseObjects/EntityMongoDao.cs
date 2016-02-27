using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AV.Development.Dal.MongoDB.DatabaseObjects
{
    public class EntityMongoDao : MongoDbObjectBase
    {
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Int32)]
        public int EntityId { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreationDate { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.String)]
        public string UniqueKey { get; set; }
        [BsonIgnoreIfNull]
        public List<AttributeDataMongoDao> AttributeData { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.String)]
        public string TypeName { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Int32)]
        public int TypeId { get; set; }

    }
}
