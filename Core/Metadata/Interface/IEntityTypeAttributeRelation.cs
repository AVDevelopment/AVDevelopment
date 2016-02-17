using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    public interface IEntityTypeAttributeRelation
    {
        /// <summary>
        /// IAttributeType interface for table 'MM_EntityTypeAttributeRelation'.
        /// </summary>
        #region Public Properties

        int ID
        {
            get;
            set;
        }

        int EntityTypeID
        {
            get;
            set;

        }
        string EntityTypeCaption
        {
            get;
            set;

        }
        int AttributeID
        {
            get;
            set;

        }

        string AttributeCaption
        {
            get;
            set;

        }
        int AttributeTypeID
        {
            get;
            set;
        }
        string ValidationID
        {
            get;
            set;

        }

        int SortOrder
        {
            get;
            set;

        }
        string DefaultValue
        {
            get;
            set;

        }

        bool InheritFromParent
        {
            get;
            set;

        }

        bool IsReadOnly
        {
            get;
            set;

        }

        bool IsSpecial
        {
            get;
            set;

        }

        bool ChooseFromParentOnly
        {
            get;
            set;

        }

        bool IsValidationNeeded
        {
            get;
            set;

        }
        string Caption
        {
            get;
            set;

        }

        bool IsSystemDefined
        {
            get;
            set;

        }
        string PlaceHolderValue
        {
            get;
            set;

        }
        int MinValue
        {
            get;
            set;

        }
        int MaxValue
        {
            get;
            set;

        }
        bool IsHelptextEnabled
        {
            get;
            set;
        }

        string HelptextDecsription
        {
            get;
            set;
        }
        dynamic ParentValue { get; set; }
        dynamic ParentTreeLevelValueCaption { get; set; }
        dynamic Lable { get; set; }
        string strAttributeID { get; set; }
        #endregion
    }

}
