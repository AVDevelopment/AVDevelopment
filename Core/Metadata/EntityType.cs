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
	/// EntityType object for table 'MM_EntityType'.
	/// </summary>
	
	internal class EntityType : IEntityType 
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

        public int ModuleID
        {
            get;
            set;

        }

        public string ModuleCaption
        {
            get;
            set;
        }



        public int Category
        {
            get;
            set;

        }

        public int Group
        {
            get;
            set;

        }

        public int? AttributeSetid
        {
            get;
            set;

        }

        public string ShortDescription
        {
            get;
            set;
        }

        public string ColorCode
        {
            get;
            set;
        }

        public virtual bool IsAssociate
        {
            get;
            set;

        }

      
        public virtual bool IsRootLevel
        {
            get;
            set;
        }

       
		#endregion 
		
	
	}
	
}
