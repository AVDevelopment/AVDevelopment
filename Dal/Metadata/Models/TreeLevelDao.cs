using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{

	/// <summary>
	/// TreeLevelDao object for table 'MM_TreeLevel'.
	/// </summary>
	
	public partial class TreeLevelDao : BaseDao, ICloneable 
	{
		
		#region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual int Level
        {
            get;
            set;

        }

        public virtual string LevelName
        {
            get;
            set;

        }

        public virtual int AttributeID
        {
            get;
            set;

        }

        public virtual bool IsPercentage
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
