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

        [BsonRepresentation(BsonType.Int32)]
        public int EntityId { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreationDate { get; set; }

    }
}
