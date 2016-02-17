using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

	/// <summary>
	/// FeatureDao object for table 'MM_Feature'.
	/// </summary>
	
	public partial class FeatureDao : BaseDao, ICloneable 
	{
		
		#region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual string Caption
        {
            get;
            set;

        }

        public virtual string Description
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
		
		#region ICloneable methods

        public virtual object Clone()
		{
			return this.MemberwiseClone();
		}
		
		#endregion
				
	}
	
}
