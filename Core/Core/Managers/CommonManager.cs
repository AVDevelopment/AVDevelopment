using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Core.Interface;
using AV.Development.Core.Managers.Proxy;
using Newtonsoft.Json.Linq;
using AV.Development.Dal.Base;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Threading.Tasks;
using AV.Development.Core.Metadata;
using System.Xml.Serialization;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using Development.Utility;
using System.Globalization;
using AV.Development.Core.User.Interface;
using AV.Development.Dal.User.Model;
using AV.Development.Dal.MongoDB.Repositories.Interface;
using AV.Development.Dal.MongoDB.Repositories;
using AV.Development.Dal.MongoDB.DatabaseObjects;
using AV.Development.Dal.MongoDB.Domain;

namespace AV.Development.Core.Managers
{
    internal partial class CommonManager : IManager
    {

        /// <summary>
        /// The instance
        /// </summary>
        private static CommonManager instance = new CommonManager();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        internal static CommonManager Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Initializes the specified Development manager.
        /// </summary>
        /// <param name="DevelopmentManager">The Development manager.</param>
        void IManager.Initialize(IDevelopmentManager DevelopmentManager)
        {
            // Cache and initialize things here...
        }

        /// <summary>
        /// Commit all caches since the transaction has been commited.
        /// </summary>
        void IManager.CommitCaches()
        {
        }

        /// <summary>
        /// Rollback all caches since the transaction has been rollbacked.
        /// </summary>
        void IManager.RollbackCaches()
        {
        }


        #region TestMethod

        #region TestMethod
        public string TestMethod(CommonManagerProxy proxy)
        {
            string response = "";
            try
            {

                using (ITransaction tx = proxy.DevelopmentManager.GetTransaction())
                {
                    response = tx.PersistenceManager.MetadataRepository.getServerDate().ToString("yyyy-MM-dd");
                    tx.Commit();
                }

                return response;

            }

            catch (DBConcurrencyException ex)
            {
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        #endregion

        #region GetEntities
        public IList<Entity> GetEntities(CommonManagerProxy proxy, int pageNo, int pageSize)
        {
            IList<Entity> response = null;
            try
            {

                using (ITransaction tx = proxy.DevelopmentManager.GetTransaction())
                {
                    //IConfigurationRepository repo = new ConfigurationRepository(new WebConfigConnectionStringRepository());  //direct access to the mongoDB
                    //var entites = repo.GetEntities(1, 10);
                    response = tx.PersistenceManager.ConfigurationRepository.GetEntities(pageNo, pageSize, true); //mongodb repository access through PersistenceManager
                    tx.Commit();
                }

                return response;

            }

            catch (DBConcurrencyException ex)
            {
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }

        }
        #endregion

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="UserPwd">The user PWD.</param>
        /// <returns>IUser</returns>
        public IUser ValidateUser(int ID)
        {
            ClsDb objDb = new ClsDb();

            return objDb.GetUserByID("SELECT * FROM UM_User  WHERE ID=" + ID + "", CommandType.Text);

        }

        #endregion

        #region TestMethod

        #region SaveError
        public bool SaveError(CommonManagerProxy proxy, ErrorDao _error)
        {
            try
            {
                using (ITransaction tx = proxy.DevelopmentManager.GetTransaction())
                {
                    tx.PersistenceManager.UserRepository.Save<ErrorDao>(_error);
                    tx.Commit();
                }

                return true;
            }

            catch (DBConcurrencyException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        #endregion


        #endregion


    }
}


