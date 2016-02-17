using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// EntityTypeFeatureDao object for table 'MM_EntityType_Feature'.
    /// </summary>

    public partial class EntityTypeFeatureDao : BaseDao, ICloneable
    {

        #region Public Properties


        public virtual int Id
        {
            get;
            set;

        }

        public virtual int TypeID
        {
            get;
            set;

        }

        public virtual int FeatureID
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
