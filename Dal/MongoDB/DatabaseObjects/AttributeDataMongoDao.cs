using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Development.Dal.MongoDB.DatabaseObjects
{
    public class AttributeDataMongoDao
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public dynamic Lable { get; set; }
        public dynamic Caption { get; set; }
        public dynamic Value { get; set; }
        public dynamic ValueCaption { get; set; }
        public string SpecialValue { get; set; }
        public int Level { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsInheritFromParent { get; set; }
        public bool IsChooseFromParent { get; set; }
        public bool IsReadOnly { get; set; }
        public int SortOrder { get; set; }
        public dynamic tree { get; set; }
        public dynamic options { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
