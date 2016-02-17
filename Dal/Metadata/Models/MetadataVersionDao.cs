using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace AV.Development.Dal.Metadata.Model
{
    public partial class MetadataVersionDao : BaseDao, ICloneable
    {

        #region Public Properties

        public virtual int ID
        {
            get;
            set;

        }

        public virtual string Name
        {
            get;
            set;

        }

        public virtual string Description
        {
            get;
            set;

        }

        public virtual int State
        {
            get;
            set;

        }

        public virtual DateTime StartDate
        {
            get;
            set;

        }

        public virtual DateTime EndDate
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
