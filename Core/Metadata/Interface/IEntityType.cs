using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IEntityType interface for table 'MM_EntityType'.
    /// </summary>
    public interface IEntityType
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        string Caption
        {
            get;
            set;

        }

        string Description
        {
            get;
            set;

        }

        int ModuleID
        {
            get;
            set;

        }

        string ModuleCaption
        {
            get;
            set;

        }

        int Category
        {
            get;
            set;

        }

        int Group
        {
            get;
            set;

        }

        int? AttributeSetid
        {
            get;
            set;

        }

        string ShortDescription
        {
            get;
            set;
        }
        string ColorCode
        {
            get;
            set;
        }

        bool IsAssociate
        {
            get;
            set;

        }
      
        bool IsRootLevel
        {
            get;
            set;
        }

        #endregion
    }
}
