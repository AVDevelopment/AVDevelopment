using System;
using System.Collections;
using System.Collections.Generic;
using AV.Development.Core.Metadata.Interface;

namespace AV.Development.Core.Metadata
{

	/// <summary>
	/// EntityTypeHierarchy object for table 'MM_EntityType_Hierarchy'.
	/// </summary>

    internal class EntityTypeHierarchy : IEntityTypeHierarchy
    {
     
        #region Public Properties


        public int Id
        {
            get;
            set;

        }

        public int ParentActivityTypeID
        {
            get;
            set;

        }

        public int ChildActivityTypeID
        {
            get;
            set;

        }

        public int SortOrder
        {
            get;
            set;

        }


        #endregion

      
    }	
}
