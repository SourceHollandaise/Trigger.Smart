using System;
using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Dependency;
using System.Linq;
using Trigger.BCL.Common.Security;
using System.Configuration;

namespace Trigger.BCL.ParaOffice
{

    public class StoreConfiguration : IStoreConfiguration
    {
        public string DataStoreLocation { get; protected set; }

        public string DocumentStoreLocation { get; protected set; }

        public  void InitStore()
        {
            SetStoreLocation();

            SetDocumentStoreLocation();
        }

        protected virtual void  SetStoreLocation()
        {
            var value = "/Users/trigger/Dropbox/ParaOffice_datastore/";//ConfigurationManager.AppSettings["DataStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DataStoreLocation = value;
        }

        protected virtual void SetDocumentStoreLocation()
        {
            var value = "/Users/trigger/Dropbox/ParaOffice_datastore/";//ConfigurationManager.AppSettings["DocumentStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DocumentStoreLocation = value;
        }
    }
}
