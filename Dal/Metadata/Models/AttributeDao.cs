using System;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// AttributeDao object for table 'MM_Attribute'.
    /// </summary>

    public class AttributeDao : BaseDao, ICloneable
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

        public virtual int AttributeTypeID
        {
            get;
            set;

        }

        public virtual bool IsSystemDefined
        {
            get;
            set;

        }


        public virtual bool IsSpecial
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
