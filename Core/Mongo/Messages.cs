using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Norm;
using Norm.Attributes;

namespace AV.Development.Core.Mongo
{

    public class headers
    {
        [MongoIgnoreIfNull]
        public DateTime Date { get; set; }
        [MongoIgnoreIfNull]
        public string From { get; set; }
        [MongoIgnoreIfNull]
        public string Subject { get; set; }
        [MongoIgnoreIfNull]
        public List<string> To { get; set; }
        [MongoIgnoreIfNull]
        public string ContentTransferEncoding { get; set; }
        [MongoIgnoreIfNull]
        public string ContentType { get; set; }
        [MongoIgnoreIfNull]
        public string MessageID { get; set; }
        [MongoIgnoreIfNull]
        public string MimeVersion { get; set; }
        [MongoIgnoreIfNull]
        public string XFileName { get; set; }
        [MongoIgnoreIfNull]
        public string XFolder { get; set; }
        [MongoIgnoreIfNull]
        public string XFrom { get; set; }
        [MongoIgnoreIfNull]
        public string XOrigin { get; set; }
        [MongoIgnoreIfNull]
        public string XTo { get; set; }
        [MongoIgnoreIfNull]
        public string Xbcc { get; set; }
        [MongoIgnoreIfNull]
        public string Bcc { get; set; }
        [MongoIgnoreIfNull]
        public string Cc { get; set; }
        [MongoIgnoreIfNull]
        public string Xcc { get; set; }
        [MongoIgnoreIfNull]
        public string Attendees { get; set; }
        [MongoIgnoreIfNull]
        public string Re { get; set; }

    }

    public class messagescopy
    {
        public ObjectId Id { get; private set; }
        [MongoIgnoreIfNull]
        public string body { get; set; }
        [MongoIgnoreIfNull]
        public string filename { get; set; }

        public headers headers { get; set; }

        public messagescopy()
        {
            headers = new headers();
        }


        [MongoIgnoreIfNull]
        public string mailbox { get; set; }
        [MongoIgnoreIfNull]
        public string subFolder { get; set; }
    }

}
