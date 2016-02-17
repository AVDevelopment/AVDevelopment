using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AV.Development.Core.Interface
{
    interface IManager
    {
        void Initialize(IDevelopmentManager DevelopmentManager);

        void CommitCaches();
        void RollbackCaches();
    }
}
