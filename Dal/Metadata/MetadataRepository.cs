using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.Base;
using NHibernate;

namespace AV.Development.Dal.Metadata
{
    public class MetadataRepository : GenericRepository
    {
        public MetadataRepository(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {

        }
        public DateTime getServerDate()
        {
            return DateTime.Now;
        }
    }
}
