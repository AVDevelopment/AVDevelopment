using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using Newtonsoft.Json.Linq;
using AV.Development.Core.Metadata;
using AV.Development.Dal.User.Model;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Domain;

namespace AV.Development.Core.Interface.Managers
{
    public interface ICommonManager
    {

        #region Instance of Classes In ServiceLayer reference
        /// <summary>
        /// Returns File class.
        /// </summary>

        #endregion

        #region Methods

        string TestMethod();
        bool SaveError(ErrorDao _error);

        #region GetEntities
        IList<Entity> GetEntities(int pageNo, int pageSize);
        #endregion

        #region GetObjects
        List<T> GetObject<T>();
        #endregion

        #region GetObjects
        List<T> GetObject<T>(string mongoVersion);
        #endregion

        #endregion

    }
}
