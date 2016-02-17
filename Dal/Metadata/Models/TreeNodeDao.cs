using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

    /// <summary>
    /// TreeNodeDao object for table 'MM_TreeNode'.
    /// </summary>

    public partial class TreeNodeDao : BaseDao, ICloneable
    {

        #region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual int NodeID
        {
            get;
            set;

        }
        public virtual int ParentNodeID
        {
            get;
            set;

        }
        public virtual int Level
        {
            get;
            set;

        }

        public virtual string KEY
        {
            get;
            set;

        }

        public virtual int AttributeID
        {
            get;
            set;

        }




        public virtual string Caption
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
