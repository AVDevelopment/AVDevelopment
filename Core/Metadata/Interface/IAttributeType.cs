using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IAttributeType interface for table 'MM_AttributeType'.
    /// </summary>
    public interface IAttributeType
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

        string ClassName
        {
            get;
            set;

        }

        bool IsSelectable
        {
            get;
            set;

        }

        string DataType
        {
            get;
            set;
        }
        string SqlType
        {
            get;
            set;
        }

        int Length
        {
            get;
            set;
        }

        bool IsNullable
        {
            get;
            set;
        }
        

        #endregion
    }
}
