using System;
using System.Collections;
using System.Collections.Generic;

namespace AV.Development.Dal.Metadata.Model
{
    public class EntityTypeAttributeRelationDao : BaseDao, ICloneable 
    {
      
		#region Public Properties
        public virtual int ID
        {
            get;
            set;
        }
        public virtual int EntityTypeID
        {
            get;
            set;

        }

        public virtual int AttributeID
        {
            get;
            set;

        }


        public virtual string ValidationID
        {
            get;
            set;
        }

        public virtual int SortOrder
        {
            get;
            set;

        }

        public virtual string DefaultValue
        {
            get;
            set;

        }

        public virtual string PlaceHolderValue
        {
            get;
            set;

        }
        public virtual int MinValue
        {
            get;
            set;
        }
        public virtual int MaxValue
        {
            get;
            set;
        }
        public virtual bool InheritFromParent
        {
            get;
            set;

        }

        public virtual bool IsReadOnly
        {
            get;
            set;

        }

        public virtual bool ChooseFromParentOnly
        {
            get;
            set;

        }

        public virtual bool IsValidationNeeded
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
        public virtual string Caption
        {
            get;
            set;

        }

        public virtual bool IsSystemDefined
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
