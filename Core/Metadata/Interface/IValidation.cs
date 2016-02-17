using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IValidation interface for table 'MM_Validation'.
    /// </summary>
    public interface IValidation
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        int EntityTypeID
        {
            get;
            set;

        }

        int RelationShipID
        {
            get;
            set;

        }

        string Name
        {
            get;
            set;

        }

        string ValueType
        {
            get;
            set;

        }

        string ErrorMessage
        {
            get;
            set;

        }

        string Value
        {
            get;
            set;

        }

        int AttributeID
        {
            get;
            set;

        }

        #endregion
    }
}
