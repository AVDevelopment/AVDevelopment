using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// AttributeTypeDao object for table 'MM_AttributeType'.
    /// </summary>

    public partial class AttributeTypeDao : BaseDao, ICloneable
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

        public virtual string ClassName
        {
            get;
            set;

        }

        public virtual bool IsSelectable
        {
            get;
            set;

        }

        public virtual string DataType
        {
            get;
            set;

        }

        public virtual string SqlType
        {
            get;
            set;

        }

        public virtual int Length
        {
            get;
            set;

        }

        public virtual bool IsNullable
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
