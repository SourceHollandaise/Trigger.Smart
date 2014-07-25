using System;
using System.IO;

namespace Trigger.XStorable.DataStore
{
    public static class StoreConfigurator
    {
        public static string DataStoreLocation;

        public static string DocumentStoreLocation;

        public static string StoreMap;

        public static void InitStore()
        {
            DataStoreLocation = SetStoreLocation();

            DocumentStoreLocation = SetDocumentStoreLocation();
        }

        static string SetStoreLocation()
        {
            var value = "/Users/trigger/Dropbox/EventTracker_datastore/";
          

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);
                
            return value;
        }

        static string SetDocumentStoreLocation()
        {
            var value = "/Users/trigger/Dropbox/EventTracker_docstore/";
           
            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            return value;
        }
    }
}
