using System;
using System.Configuration;
using System.IO;

namespace Trigger.CRM.Persistent
{
    public static class PersistentStoreInitialzer
    {
        internal static string PersistentStoreLocation;

        internal static string PersistentStoreMap;

        public static void InitStore()
        {
            PersistentStoreLocation = SetStoreLocation();

            PersistentStoreMap = CreateStoreMap();
        }

        static string SetStoreLocation()
        {
            var defaultDirectory = ConfigurationManager.AppSettings["PersistentStoreLocation"];



            if (!Directory.Exists(defaultDirectory))
                Directory.CreateDirectory(defaultDirectory);
                
            return defaultDirectory;
        }

        static string CreateStoreMap()
        {
            var typeMapFile = PersistentStoreLocation + ConfigurationManager.AppSettings["PersistentStoreMap"];

            if (!File.Exists(typeMapFile))
            {
                File.Create(typeMapFile);
                System.Threading.Thread.Sleep(2000);
            }
                
            return typeMapFile;
        }
    }
}
