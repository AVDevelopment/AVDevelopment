using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// ITreeLevel interface for table 'MM_TreeLevel'.
     
    /// </summary>
    public interface ITreeLevel
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        int Level
        {
            get;
            set;

        }

        string LevelName
        {
            get;
            set;

        }

        int AttributeID
        {
            get;
            set;

        }

        bool IsPercentage
        {
            get;
            set;
        
        }
        

        #endregion
    }
}
