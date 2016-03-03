using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AV.Development.Dal.Base;
using AV.Development.Dal.Metadata.Model;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using MongoDB.Bson;
using NHibernate;

namespace AV.Development.Dal.Metadata
{
    public class MetadataRepository : GenericRepository
    {
        public MetadataRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {

        }
        public DateTime getServerDate()
        {
            return DateTime.Now;
        }
        public List<T> GetObject<T>(string xmlpath)
        {
            try
            {
                if (File.Exists(xmlpath))
                {

                    //// the using statement implicitally calls the dispose on exit
                    using (var DisposableObject = new DisposableClass(xmlpath))
                    {

                        XDocument xDoc = XDocument.Load(xmlpath);
                        List<T> obj = null;
                        if (typeof(T).Name == "EntityTypeMongoDao" && xDoc.Descendants("EntityType").Count() > 0)
                        {

                            obj = xDoc.Root.Elements("EntityType_Table").Elements("EntityType").Select(e => new EntityTypeMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                EntityTypeId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                Description = e.Element("Description").Value,
                                ModuleID = Convert.ToInt32(e.Element("ModuleID").Value),
                                Category = Convert.ToInt32(e.Element("Category").Value),
                                ShortDescription = e.Element("ShortDescription").Value,
                                ColorCode = e.Element("ColorCode").Value,
                                IsAssociate = Convert.ToBoolean(Convert.ToInt32(e.Element("IsAssociate").Value)),
                                IsRootLevel = Convert.ToBoolean(Convert.ToInt32(e.Element("IsRootLevel").Value))
                            }).Cast<T>().ToList();

                        }

                        else if (typeof(T).Name == "FeatureMongoDao" && xDoc.Descendants("Feature").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("Feature_Table").Elements("Feature").Select(e => new FeatureMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                FeatureId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                Description = e.Element("Description").Value
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "ModuleMongoDao" && xDoc.Descendants("Module").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("Module_Table").Elements("Module").Select(e => new ModuleMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                ModuleId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                Description = e.Element("Description").Value,
                                IsEnable = Convert.ToBoolean(Convert.ToInt32(e.Element("IsEnable").Value))
                            }).Cast<T>().ToList();
                        }

                        else if (typeof(T).Name == "EntitytypeFeatureMongoDao" && xDoc.Descendants("EntityType_Feature").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("EntityType_Feature_Table").Elements("EntityType_Feature").Select(e => new EntitytypeFeatureMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                TypeFeatureId = Convert.ToInt32(e.Element("ID").Value),
                                TypeID = Convert.ToInt32(e.Element("TypeID").Value),
                                FeatureID = Convert.ToInt32(e.Element("FeatureID").Value)
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "EntityTypeAttributeRelationMongoDao" && xDoc.Descendants("EntityTypeAttributeRelation").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("EntityTypeAttributeRelation_Table").Elements("EntityTypeAttributeRelation").Select(e => new EntityTypeAttributeRelationMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                RelationID = Convert.ToInt32(e.Element("ID").Value),
                                EntityTypeID = Convert.ToInt32(e.Element("EntityTypeID").Value),
                                AttributeID = Convert.ToInt32(e.Element("AttributeID").Value),
                                ValidationID = e.Element("ValidationID").Value,
                                SortOrder = Convert.ToInt32(e.Element("SortOrder").Value),
                                DefaultValue = e.Element("DefaultValue").Value,
                                InheritFromParent = Convert.ToBoolean(Convert.ToInt32(e.Element("InheritFromParent").Value)),
                                IsReadOnly = Convert.ToBoolean(Convert.ToInt32(e.Element("IsReadOnly").Value)),
                                ChooseFromParentOnly = Convert.ToBoolean(Convert.ToInt32(e.Element("ChooseFromParentOnly").Value)),
                                IsValidationNeeded = Convert.ToBoolean(Convert.ToInt32(e.Element("IsValidationNeeded").Value)),
                                Caption = e.Element("Caption").Value,
                                IsSystemDefined = Convert.ToBoolean(Convert.ToInt32(e.Element("IsSystemDefined").Value)),
                                PlaceHolderValue = e.Element("PlaceHolderValue").Value,
                                MinValue = Convert.ToInt32(e.Element("MinValue").Value),
                                MaxValue = Convert.ToInt32(e.Element("MaxValue").Value)
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "AttributeTypeMongoDao" && xDoc.Descendants("AttributeType").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("AttributeType_Table").Elements("AttributeType").Select(e => new AttributeTypeMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                AttributeTypeId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                ClassName = e.Element("ClassName").Value,
                                IsSelectable = Convert.ToBoolean(Convert.ToInt32(e.Element("IsSelectable").Value)),
                                DataType = e.Element("DataType").Value,
                                SqlType = e.Element("SqlType").Value,
                                Length = Convert.ToInt32(e.Element("Length").Value),
                                IsNullable = Convert.ToBoolean(Convert.ToInt32(e.Element("IsNullable").Value))
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "AttributeMongoDao" && xDoc.Descendants("Attribute").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("Attribute_Table").Elements("Attribute").Select(e => new AttributeMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                AttributeId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                Description = e.Element("Description").Value,
                                AttributeTypeID = Convert.ToInt32(e.Element("AttributeTypeID").Value),
                                IsSystemDefined = Convert.ToBoolean(Convert.ToInt32(e.Element("IsSystemDefined").Value)),
                                IsSpecial = Convert.ToBoolean(Convert.ToInt32(e.Element("IsSpecial").Value))
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "OptionMongoDao" && xDoc.Descendants("Option").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("Option_Table").Elements("Option").Select(e => new OptionMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                OptionId = Convert.ToInt32(e.Element("ID").Value),
                                Caption = e.Element("Caption").Value,
                                AttributeID = Convert.ToInt32(e.Element("AttributeID").Value),
                                SortOrder = Convert.ToInt32(e.Element("SortOrder").Value)
                            }).Cast<T>().ToList();
                        }

                        else if (typeof(T).Name == "TreeLevelMongoDao" && xDoc.Descendants("TreeLevel").Count() > 0)
                        {

                            obj = xDoc.Root.Elements("TreeLevel_Table").Elements("TreeLevel").Select(e => new TreeLevelMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                TreeLevelId = Convert.ToInt32(e.Element("ID").Value),
                                Level = Convert.ToInt32(e.Element("Level").Value),
                                LevelName = e.Element("LevelName").Value,
                                IsPercentage = Convert.ToBoolean(Convert.ToInt32(e.Element("IsPercentage").Value)),
                                AttributeID = Convert.ToInt32(e.Element("AttributeID").Value)
                            }).Cast<T>().ToList();

                        }
                        else if (typeof(T).Name == "TreeNodeMongoDao" && xDoc.Descendants("TreeNode").Count() > 0)
                        {

                            obj = xDoc.Root.Elements("TreeNode_Table").Elements("TreeNode").Select(e => new TreeNodeMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                TreeNodeId = Convert.ToInt32(e.Element("ID").Value),
                                NodeID = Convert.ToInt32(e.Element("NodeID").Value),
                                ParentNodeID = Convert.ToInt32(e.Element("ParentNodeID").Value),
                                Level = Convert.ToInt32(e.Element("Level").Value),
                                KEY = e.Element("KEY").Value,
                                AttributeID = Convert.ToInt32(e.Element("AttributeID").Value),
                                Caption = e.Element("Caption").Value,
                                SortOrder = Convert.ToInt32(e.Element("SortOrder").Value)
                            }).Cast<T>().ToList();

                        }

                        else if (typeof(T).Name == "ValidationMongoDao" && xDoc.Descendants("Validation").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("Validation_Table").Elements("Validation").Select(e => new ValidationMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                ValidationId = Convert.ToInt32(e.Element("ID").Value),
                                EntityTypeID = Convert.ToInt32(e.Element("EntityTypeID").Value),
                                RelationShipID = Convert.ToInt32(e.Element("RelationShipID").Value),
                                Name = e.Element("Name").Value,
                                ValueType = e.Element("ValueType").Value,
                                Value = e.Element("Value").Value,
                                ErrorMessage = e.Element("ErrorMessage").Value
                            }).Cast<T>().ToList();
                        }
                        else if (typeof(T).Name == "EntityTypeHierarchyMongoDao" && xDoc.Descendants("EntityType_Hierarchy").Count() > 0)
                        {
                            obj = xDoc.Root.Elements("EntityType_Hierarchy_Table").Elements("EntityType_Hierarchy").Select(e => new EntityTypeHierarchyMongoDao
                            {
                                DbObjectId = ObjectId.GenerateNewId(),
                                HierarchyId = Convert.ToInt32(e.Element("ID").Value),
                                ParentActivityTypeID = Convert.ToInt32(e.Element("ParentActivityTypeID").Value),
                                ChildActivityTypeID = Convert.ToInt32(e.Element("ChildActivityTypeID").Value),
                                SortOrder = Convert.ToInt32(e.Element("SortOrder").Value)
                            }).Cast<T>().ToList();
                        }

                        return obj;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public int GetMaxId<T>()
        {
            int maxID = 0;
            try
            {
                if (typeof(T).Name == "EntityTypeMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_EntityType_seq;");
                }

                else if (typeof(T).Name == "FeatureMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_Feature_seq;");
                }

                else if (typeof(T).Name == "ModuleMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_Module_seq;");
                }

                else if (typeof(T).Name == "EntitytypeFeatureMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_EntitytypeFeature_seq;");
                }

                else if (typeof(T).Name == "EntityTypeAttributeRelationMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_EntityTypeAttributeRelation_seq;");
                }
                else if (typeof(T).Name == "AttributeTypeMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_AttributeType_seq;");
                }
                else if (typeof(T).Name == "AttributeMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_Attribute_seq;");
                }
                else if (typeof(T).Name == "OptionMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_Option_seq;");
                }

                else if (typeof(T).Name == "TreeLevelMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_TreeLevel_seq;");
                }
                else if (typeof(T).Name == "TreeNodeMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_TreeNode_seq;");
                }

                else if (typeof(T).Name == "ValidationMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_Validation_seq;");
                }
                else if (typeof(T).Name == "EntityTypeHierarchyMongoDao")
                {
                    maxID = PersistenceManager.Instance.MetadataRepository.GetNextValueOfSequence("SELECT NEXT VALUE FOR MM_EntityTypeHierarchy_seq;");
                }
            }

            catch
            {

            }

            return maxID;
        }

    }
}
