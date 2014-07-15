using System;
using System.Configuration;
using System.IO;

namespace Trigger.CRM.Persistent
{
    public static class StoreConfigurator
    {
        internal static string PersistentStoreLocation;

        internal static string PersistentDocumentStoreLocation;

        internal static string PersistentStoreMap;

        public static void InitStore()
        {
            PersistentStoreLocation = SetStoreLocation();

            PersistentDocumentStoreLocation = SetDocumentStoreLocation();

            PersistentStoreMap = CreateStoreMap();
        }

        static string SetStoreLocation()
        {
            var defaultDirectory = ConfigurationManager.AppSettings["PersistentStoreLocation"];

            if (!Directory.Exists(defaultDirectory))
                Directory.CreateDirectory(defaultDirectory);
                
            return defaultDirectory;
        }

        static string SetDocumentStoreLocation()
        {
            var defaultDirectory = ConfigurationManager.AppSettings["PersistentDocumentStoreLocation"];

            if (!Directory.Exists(defaultDirectory))
                Directory.CreateDirectory(defaultDirectory);

            return defaultDirectory;
        }

        static string CreateStoreMap()
        {
            var typeMapFile = PersistentStoreLocation + ConfigurationManager.AppSettings["PersistentStoreMap"];

            if (!File.Exists(typeMapFile))
                File.Create(typeMapFile);
                
            return typeMapFile;
        }
    }
}
