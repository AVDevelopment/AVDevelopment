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
	/// Feature object for table 'MM_Feature'.
	/// </summary>
	
	internal class Feature : IFeature 
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

        public string Description
        {
            get;
            set;
        }

        public virtual int ModuleID
        {
            get;
            set;

        }

        public virtual bool IsEnable
        {
            get;
            set;

        }
        public virtual bool IsTopNavigation
        {
            get;
            set;

        }
                 

		#endregion 
		
	}
	
}
