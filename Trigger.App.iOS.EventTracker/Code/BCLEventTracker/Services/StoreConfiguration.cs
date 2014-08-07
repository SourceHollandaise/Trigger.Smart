
using System;
using Trigger.BCL.EventTracker.Model;
using System.Configuration;
using Trigger.XStorable.DataStore;
using System.IO;

namespace Trigger.BCL.EventTracker.Services
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
            var value = "/Users/trigger/Dropbox/EventTracker_datastore/";//ConfigurationManager.AppSettings["DataStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DataStoreLocation = value;
        }

        protected virtual void SetDocumentStoreLocation()
        {
            var value = "/Users/trigger/Dropbox/EventTracker_docstore/";//ConfigurationManager.AppSettings["DocumentStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DocumentStoreLocation = value;
        }
    }
    
}
