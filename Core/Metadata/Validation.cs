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
    /// Validation object for table 'MM_Validation'.
	/// </summary>
	
	internal class Validation : IValidation 
	{
		
		#region Public Properties

        public int Id
        {
            get;
            set;

        }

        public virtual int EntityTypeID
        {
            get;
            set;

        }

        public virtual int RelationShipID
        {
            get;
            set;

        }

        public virtual string Name
        {
            get;
            set;

        }

        public virtual string ValueType
        {
            get;
            set;

        }

        public virtual string ErrorMessage
        {
            get;
            set;

        }

        public virtual string Value
        {
            get;
            set;
        }



        public virtual int AttributeID
        {
            get;
            set;

        }






		#endregion 
		
	
	}
	
}
