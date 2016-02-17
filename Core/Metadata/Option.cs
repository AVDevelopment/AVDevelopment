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
    /// Option object for table 'MM_Option'.
	/// </summary>
	
	internal class Option : IOption 
	{
		
		#region Public Properties

        public int Id
        {
            get;
            set;

        }

        public string Caption
        {
            get;
            set;
        }

        public virtual int AttributeID
        {
            get;
            set;

        }

        public virtual int AttributeTypeID
        {
            get;
            set;

        }

        public virtual int SortOrder
        {
            get;
            set;

        }
		
		
		

		#endregion 
		
	}
	
}
