using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// ITreeNode interface for table 'MM_TreeNode'.

    /// </summary>
    public interface ITreeNode
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        int NodeID
        {
            get;
            set;

        }
        int ParentNodeID
        {
            get;
            set;

        }
        int Level
        {
            get;
            set;

        }

        string KEY
        {
            get;
            set;

        }

        int AttributeID
        {
            get;
            set;

        }

        string Caption
        {
            get;
            set;

        }

        int SortOrder
        {
            get;
            set;
        }

        string ColorCode
        {
            get;
            set;
        }


        #endregion
    }
}
