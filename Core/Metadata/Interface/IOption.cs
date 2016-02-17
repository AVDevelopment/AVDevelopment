using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IOption interface for table 'MM_Option'.
    /// </summary>
    public interface IOption
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

        int AttributeID
        {
            get;
            set;

        }

        int AttributeTypeID
        {
            get;
            set;

        }

        int SortOrder
        {
            get;
            set;

        }
       

        
        

        #endregion
    }
}
