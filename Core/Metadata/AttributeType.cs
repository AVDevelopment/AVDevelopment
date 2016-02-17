using System;
using System.Collections;
using System.Collections.Generic;
using AV.Development.Core.Metadata.Interface;

namespace AV.Development.Core.Metadata
{

	/// <summary>
	/// AttributeType object for table 'MM_AttributeType'.
	/// </summary>
	
	internal class AttributeType : IAttributeType 
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

        public string ClassName
        {
            get;
            set;

        }

        public bool IsSelectable
        {
            get;
            set;

        }

        public string DataType
        {
            get;
            set;

        }

        public string SqlType
        {
            get;
            set;

        }

        public int Length
        {
            get;
            set;

        }

        public bool IsNullable
        {
            get;
            set;

        }
		

		#endregion 
		
	
	}
	
}
