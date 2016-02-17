using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IEntityTypeHierarchy interface for table 'MM_EntityType_Hierarchy'.
    /// </summary>
    public interface IEntityTypeHierarchy
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        int ParentActivityTypeID
        {
            get;
            set;

        }

        int ChildActivityTypeID
        {
            get;
            set;

        }

        int SortOrder
        {
            get;
            set;

        }

        
        

        #endregion
    }
}
