using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// ModuleDao object for table 'MM_Module'.
    /// </summary>

    public partial class ModuleDao : BaseDao, ICloneable
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

        public virtual bool IsEnable
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
