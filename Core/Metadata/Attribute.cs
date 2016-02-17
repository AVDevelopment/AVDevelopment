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
	/// Attribute object for table 'MM_Attribute'.
	/// </summary>
	
	internal class Attribute : IAttribute 
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
        public string Type
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int AttributeTypeID
        {
            get;
            set;

        }

        public bool IsSystemDefined
        {
            get;
            set;

        }
		
		
        public bool IsSpecial
        {
            get;
            set;

        }

        public int Level
        {
            get;
            set;
        }
     
		#endregion 
		
	
	}
	
}
