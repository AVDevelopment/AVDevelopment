using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IAttribute interface for table 'MM_Attribute'.
    /// </summary>
    public interface IAttribute
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

        int AttributeTypeID
        {
            get;
            set;

        }

        bool IsSystemDefined
        {
            get;
            set;

        }

        

        bool IsSpecial
        {
            get;
            set;

        }

        int Level
        {
            get;
            set;
        }
        string Type
        {
            get;
            set;
        }

        #endregion
    
    }
}
