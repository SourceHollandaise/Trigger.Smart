using System;
using System.IO;
using System.Collections.Generic;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{

    public class XmlPersistentStore<T> : IPersistentStore<T> where T: IPersistentId
    {
        static string DefaultDirectory
        {
            get
            {
                return PersistentStoreInitialzer.PersistentStoreLocation;
            }
        }

        static string TypeMapFile
        {
            get
            {
                return PersistentStoreInitialzer.PersistentStoreMap;
            }
        }

        protected virtual object GetId()
        {
            return Guid.NewGuid();
        }

        public string Save(T item)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                if (item.Id == null)
                    item.Id = DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

                var	json = ServiceStack.Text.XmlSerializer.SerializeToString<T>(item);
                var path = DefaultDirectory + "/" + item.Id + ".xml";

                if (!File.Exists(path))
                    SaveToTypeMap(typeof(T), path, item.Id.ToString());

                File.WriteAllText(path, json);

                return path;
            }

            return null;
        }

        public T Load(object itemId)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                var path = DefaultDirectory + "/" + itemId + ".xml";

                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    return ServiceStack.Text.XmlSerializer.DeserializeFromString<T>(content);

                }
                return default(T);
            }

            return default(T);
        }

        public void Delete(object itemId)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                var path = DefaultDirectory + "/" + itemId + ".xml";

                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public IEnumerable<T>LoadAll()
        {
            if (File.Exists(TypeMapFile))
            {
                var lines = File.ReadAllLines(TypeMapFile);

                foreach (var line in lines)
                {
                    var splitted = line.Split(';');
                    if (splitted[0] == typeof(T).FullName)
                    {
                        var item = Load(splitted[1]);
                        if (item != null)
                            yield return item;
                    }
                }
            }
        }

        static T Load(string path)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                return ServiceStack.Text.XmlSerializer.DeserializeFromString<T>(content);
            }

            return default(T);
        }

        static void SaveToTypeMap(Type type, string file, string id)
        {
            if (Directory.Exists(DefaultDirectory) && File.Exists(TypeMapFile))
                File.AppendAllText(TypeMapFile, type.FullName + ";" + file + ";" + id + Environment.NewLine);
        }
    }
}
