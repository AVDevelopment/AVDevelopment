using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

	/// <summary>
    /// ValidationDao object for table 'MM_Validation'.
	/// </summary>
	
	public partial class ValidationDao : BaseDao, ICloneable 
	{
		
		#region Public Properties

        public virtual int Id
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

        public virtual bool IsDeleted
        {
            get;
            set;
        }

        public virtual bool IsChanged
        {
            get;
            set;
        }
		
		#endregion 				
		
		#region ICloneable methods

        public virtual object Clone()
		{
			return this.MemberwiseClone();
		}
		
		#endregion
				
		
	}
	
}
