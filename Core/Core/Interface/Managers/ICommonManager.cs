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

namespace AV.Development.Core.Interface.Managers
{
    public interface ICommonManager
    {

        #region Instance of Classes In ServiceLayer reference
        /// <summary>
        /// Returns File class.
        /// </summary>

        #endregion

        #region TestMethod
        string TestMethod();
        bool SaveError(ErrorDao _error);

        #endregion

    }
}
