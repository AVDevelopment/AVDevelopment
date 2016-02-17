using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// OptioDao object for table 'MM_Option'.
    /// </summary>

    public partial class OptionDao : BaseDao, ICloneable
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

        public virtual int AttributeID
        {
            get;
            set;

        }

        public virtual int SortOrder
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
