using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Core.Interface.Managers;
using AV.Development.Core.User.Interface;

using System.Collections;

namespace AV.Development.Core.Interface
{
    public interface IDevelopmentManager
    {
        IUser User { get; set; }

        int EntitySortorderIdColleHash { get; set; }
        string EntityMainQuery { get; set; }
        Tuple<ArrayList, ArrayList> GeneralColumnDefs { get; set; }

        ICommonManager CommonManager { get; }
       
        IEventManager EventManager { get; }
        IPluginManager PluginManager { get; }

        ITransaction GetTransaction();
        ITransaction GetTransaction(bool create);

        bool PowerMode { get; set; }
    }
}
