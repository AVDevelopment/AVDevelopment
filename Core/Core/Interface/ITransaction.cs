using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AV.Development.Dal.Base;

namespace AV.Development.Core.Interface
{
    public interface ITransaction : IDisposable
    {
        bool IsOpen { get; }

        void Commit();
        void Rollback();

        PersistenceManager PersistenceManager { get; }
    }
}
