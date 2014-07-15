using System;
using System.Configuration;
using System.IO;

namespace Trigger.CRM.Persistent
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

            StoreMap = CreateStoreMap();
        }

        static string SetStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DataStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);
                
            return value;
        }

        static string SetDocumentStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DocumentStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            return value;
        }

        static string CreateStoreMap()
        {
            var value = DataStoreLocation + ConfigurationManager.AppSettings["StoreMap"];

            if (!File.Exists(value))
                File.Create(value);
                
            return value;
        }
    }
}
