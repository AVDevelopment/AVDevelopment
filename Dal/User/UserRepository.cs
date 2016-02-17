using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Dal.Base;
using NHibernate;

namespace AV.Development.Dal.User
{
   public class UserRepository:GenericRepository
    {
        public UserRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}
