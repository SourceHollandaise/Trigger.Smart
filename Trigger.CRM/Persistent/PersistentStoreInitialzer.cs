using System;
using System.IO;
using System.Configuration;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{
    public static class PersistentStoreInitialzer
    {
        internal static string PersistentStoreLocation;

        internal static string PersistentStoreMap;

        public static void InitStore(string initDirectory)
        {
            PersistentStoreLocation = CreateStoreLocation(initDirectory);

            PersistentStoreMap = CreateStoreMap(PersistentStoreLocation);

            DependencyMapProvider.Instance.RegisterType<IdGenerator, GuidIdGenerator>();
        }

        static string CreateStoreLocation(string directory)
        {
            var defaultDirectory = directory;

            if (!Directory.Exists(defaultDirectory))
                Directory.CreateDirectory(defaultDirectory);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add("PersistentStoreLocation", defaultDirectory);

            config.Save(ConfigurationSaveMode.Full);

            return defaultDirectory;
        }

        static string CreateStoreMap(string directory)
        {
            var typeMapFile = directory + "/typeMap.json";

            if (!File.Exists(typeMapFile))
                File.Create(typeMapFile);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add("PersistentStoreMap", typeMapFile);

            config.Save(ConfigurationSaveMode.Full);

            return typeMapFile;
        }
    }
}
