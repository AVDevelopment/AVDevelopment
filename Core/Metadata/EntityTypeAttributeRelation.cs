using System;
using System.Collections;
using System.Collections.Generic;
using AV.Development.Core.Metadata.Interface;

namespace AV.Development.Core.Metadata
{
    internal class EntityTypeAttributeRelation : IEntityTypeAttributeRelation
    {

        /// <summary>
        /// EntityPeriod object for table 'MM_EntityTypeAttributeRelation'.
        /// </summary>
      
        #region Public Properties

        public virtual int ID
        {
            get;
            set;
        }

        public int EntityTypeID
        {
            get;
            set;

        }
        public string EntityTypeCaption
        {
            get;
            set;
        }
        public int AttributeID
        {
            get;
            set;

        }

        public string AttributeCaption
        {
            get;
            set;

        }
        public int AttributeTypeID
        {
            get;
            set;

        }
        public string ValidationID
        {
            get;
            set;

        }

        public int SortOrder
        {
            get;
            set;

        }
        public virtual string DefaultValue
        {
            get;
            set;

        }
        public virtual string PlaceHolderValue
        {
            get;
            set;

        }
        public virtual int MinValue
        {
            get;
            set;

        }
        public virtual int MaxValue
        {
            get;
            set;

        }
        public virtual bool IsHelptextEnabled
        {
            get;
            set;
        }

        public virtual string HelptextDecsription
        {
            get;
            set;
        }

        public virtual bool InheritFromParent
        {
            get;
            set;

        }

        public virtual bool IsReadOnly
        {
            get;
            set;

        }

        public virtual bool IsSpecial
        {
            get;
            set;

        }

        public virtual bool ChooseFromParentOnly
        {
            get;
            set;

        }

        public virtual bool IsValidationNeeded
        {
            get;
            set;

        }
        public virtual string Caption
        {
            get;
            set;

        }

        public virtual bool IsSystemDefined
        {
            get;
            set;

        }
        public dynamic ParentValue { get; set; }
        public dynamic ParentTreeLevelValueCaption { get; set; }
        public dynamic Lable { get; set; }
        public string strAttributeID { get; set; }
        #endregion

       
    }
   
}
