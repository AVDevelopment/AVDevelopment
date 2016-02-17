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
    /// Module object for table 'MM_TreeLevel'.
    /// </summary>

    internal class TreeLevel : ITreeLevel
    {

        #region Public Properties

        public int Id
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

    }

}
