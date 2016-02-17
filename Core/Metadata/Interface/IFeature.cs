using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IFeature interface for table 'MM_Feature'.
    /// </summary>
    public interface IFeature
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        string Caption
        {
            get;
            set;

        }

        string Description
        {
            get;
            set;

        }

        int ModuleID
        {
            get;
            set;

        }

        bool IsEnable
        {
            get;
            set;

        }
        bool IsTopNavigation
        {
            get;
            set;

        }
     
        #endregion
    }
}
