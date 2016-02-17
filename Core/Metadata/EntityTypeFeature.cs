/*
Created using Microdesk MyGeneration NHibernate Template v1.1
[based on MyGeneration/Template/NHibernate (c) by Sharp 1.4]
*/
using System;
using System.Collections;
using System.Collections.Generic;
using AV.Development.Core.Metadata.Interface;

namespace AV.Development.Core.Metadata
{

    /// <summary>
    /// EntityTypeFeature object for table 'MM_EntityType_Feature'.
    /// </summary>

    internal class EntityTypeFeature : IEntityTypeFeature
    {
      
        #region Public Properties

        public int Id
        {
            get;
            set;

        }

        public int TypeID
        {
            get;
            set;

        }

        public int FeatureID
        {
            get;
            set;

        }

        public string FeatureName
        {
            get;
            set;
        }

        #endregion

      
    }

}
