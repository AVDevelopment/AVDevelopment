using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// EntityTypeDao object for table 'MM_EntityType'.
    /// </summary>

    public partial class EntityTypeDao : BaseDao, ICloneable
    {
        #region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual string Caption
        {
            get;
            set;

        }

        public virtual string Description
        {
            get;
            set;

        }

        public virtual int ModuleID
        {
            get;
            set;

        }


        public virtual bool IsSystemDefined
        {
            get;
            set;

        }

        public virtual string ShortDescription
        {
            get;
            set;

        }

        public virtual string ColorCode
        {
            get;
            set;
        }

        public virtual bool IsAssociate
        {
            get;
            set;

        }

        public virtual int Category
        {
            get;
            set;

        }

        public virtual bool IsRootLevel
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
