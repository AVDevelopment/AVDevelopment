using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AV.Development.Dal.MongoDB.Repositories.Interface
{
    public interface IMongoConnectionStringRepository
    {
        string ReadConnectionString(string connectionStringName);
    }
}
