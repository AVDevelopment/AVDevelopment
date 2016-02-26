using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Development.Dal.MongoDB.Repositories.Interface;

namespace AV.Development.Dal.MongoDB.Repositories
{
    public class WebConfigConnectionStringRepository : IMongoConnectionStringRepository
    {
        public string ReadConnectionString(string connectionStringName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}
