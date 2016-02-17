using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// EntityTypeHierarchyDao object for table 'MM_EntityType_Hierarchy'.
    /// </summary>

    public partial class EntityTypeHierarchyDao : BaseDao, ICloneable
    {

        #region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual int ParentActivityTypeID
        {
            get;
            set;

        }

        public virtual int ChildActivityTypeID
        {
            get;
            set;

        }

        public virtual int SortOrder
        {
            get;
            set;

        }


        public virtual bool IsDeleted
        {
            get;
            set;
        }

        public virtual bool IsChanged
        {
            get;
            set;
        }

        #endregion

        #region ICloneable methods

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

    }

}
