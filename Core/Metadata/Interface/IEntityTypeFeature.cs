using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Core.Metadata.Interface
{
    /// <summary>
    /// IEntityTypeFeature interface for table 'MM_EntityType_Feature'.
    /// </summary>
    public interface IEntityTypeFeature
    {
        #region Public Properties

        int Id
        {
            get;
            set;

        }

        int TypeID
        {
            get;
            set;

        }

        int FeatureID
        {
            get;
            set;

        }
        string FeatureName
        {
            get;
            set;
        }



        #endregion
    }
}
