using System;
using System.Configuration;
using System.IO;

namespace Trigger.XStorable.DataStore
{
    public interface IStoreConfiguration
    {
        string DataStoreLocation { get; }

        string DocumentStoreLocation { get; }

        void InitStore();
    }
    
}
