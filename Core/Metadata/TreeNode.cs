/*
Created using Microdesk MyGeneration NHibernate Template v1.1
[based on MyGeneration/Template/NHibernate (c) by Sharp 1.4]
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AV.Development.Core.Metadata.Interface;

namespace AV.Development.Core.Metadata
{

	/// <summary>
    /// Module object for table 'MM_TreeNode'.
	/// </summary>
	
	public class TreeNode : ITreeNode
	{
	
		
		#region Public Properties

        public virtual int Id
        {
            get;
            set;

        }

        public virtual int NodeID
        {
            get;
            set;

        }
        public virtual int ParentNodeID
        {
            get;
            set;

        }
        public virtual int Level
        {
            get;
            set;

        }

        public virtual string KEY
        {
            get;
            set;

        }

        public virtual string ColorCode
        {
            get;
            set;

        }

        public virtual int AttributeID
        {
            get;
            set;

        }

        public virtual string Caption
        {
            get;
            set;

        }

        public virtual int SortOrder
        {
            get;
            set;

        }

        public List<TreeNode> Children { get; set; }
		

		#endregion 
		
		
		
	}

    public class UITreeNode
    {

        public string Caption { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int id { get; set; }
        public int AttributeId { get; set; }
        public bool IsDeleted { get; set; }
        public int SortOrder { get; set; }
        public bool ischecked { get; set; }
        public bool isShow { get; set; }
        public string Key { get; set; }
        public string ColorCode { get; set; }
        public List<UITreeNode> Children { get; set; }
    }

}
