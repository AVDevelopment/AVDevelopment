using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Norm;
using Norm.Attributes;

namespace AV.Development.Core.Mongo
{


    public class messagescopy_copy1
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        [BsonIgnoreIfNull]
        public string body { get; set; }
        [BsonIgnoreIfNull]
        public string filename { get; set; }

        public headers headers { get; set; }

        public messagescopy_copy1()
        {
            headers = new headers();
        }

        [BsonIgnoreIfNull]
        public string mailbox { get; set; }
        [BsonIgnoreIfNull]
        public string subFolder { get; set; }
    }

}
