using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using AV.Development.Dal.User;
using System.IO;
using System.Web;
using System.Xml;
using NHibernate.Tool.hbm2ddl;
using AV.Development.Dal.Metadata;
using AV.Development.Dal.MongoDB.Repositories;
namespace AV.Development.Dal.Base
{
    public class PersistenceManager : IDisposable
    {
        private static PersistenceManager _instance = new PersistenceManager();
        private ISessionFactory _sessionFactory = null;


        private UserRepository _userRepository = null;

        private MetadataRepository _metadataRepository = null;

        private ConfigurationRepository _configurationMongoRepository = null;



        public static PersistenceManager Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Configures NHibernate and creates a member-level session factory.
        /// </summary>
        public void Initialize()
        {
            // Initialize
            Configuration cfg = new Configuration();
            cfg.Configure();

            // Add class mappings to configuration object

            cfg.AddAssembly(this.GetType().Assembly);


            // Create session factory from configuration object
            _sessionFactory = cfg.BuildSessionFactory();

            _userRepository = new UserRepository(_sessionFactory);

            _metadataRepository = new MetadataRepository(_sessionFactory);

            _configurationMongoRepository = new ConfigurationRepository(new WebConfigConnectionStringRepository());



        }

        public void BeginTransaction()
        {
            ISession session = _sessionFactory.OpenSession();
            session.CacheMode = CacheMode.Ignore;
            session.FlushMode = FlushMode.Commit;
            CurrentSessionContext.Bind(session);
            session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                if (CurrentSessionContext.HasBind(_sessionFactory))
                {
                    using (ISession session = _sessionFactory.GetCurrentSession())
                    {
                        session.Transaction.Commit();
                        CurrentSessionContext.Unbind(_sessionFactory);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RollbackTransaction()
        {
            if (_sessionFactory != null)
            {
                if (CurrentSessionContext.HasBind(_sessionFactory))
                {
                    using (ISession session = _sessionFactory.GetCurrentSession())
                    {
                        session.Transaction.Rollback();
                        CurrentSessionContext.Unbind(_sessionFactory);
                    }
                }
            }
        }

        public void Close()
        {

        }


        public void Dispose()
        {
            //this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        //User Manager
        public UserRepository UserRepository
        {
            get { return _userRepository; }
        }

        //Metadata Manager
        public MetadataRepository MetadataRepository
        {
            get { return _metadataRepository; }
        }

        //MongoDb configuration Repository
        public ConfigurationRepository ConfigurationRepository
        {
            get { return _configurationMongoRepository; }
        }


    }
}
