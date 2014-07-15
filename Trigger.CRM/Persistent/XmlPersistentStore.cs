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

        public void Save(T item)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                if (item.Id == null)
                    item.Id = DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

                var	json = ServiceStack.Text.XmlSerializer.SerializeToString<T>(item);
                var path = Path.Combine(DefaultDirectory, item.Id + ".xml");

                if (!File.Exists(path))
                    SaveToTypeMap(typeof(T), item.Id);
                    
                File.WriteAllText(path, json);
            }
        }

        public T Load(object itemId)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                var path = Path.Combine(DefaultDirectory, itemId + ".xml");

                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    return ServiceStack.Text.XmlSerializer.DeserializeFromString<T>(content);
                }
            }

            return default(T);
        }

        public void Delete(object itemId)
        {
            if (Directory.Exists(DefaultDirectory))
            {
                var path = Path.Combine(DefaultDirectory, itemId + ".xml");

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
                        var fileName = Path.Combine(DefaultDirectory, splitted[1] + ".xml");
                        if (File.Exists(fileName))
                        {
                            var item = Load(fileName);
                            if (item != null)
                                yield return item;
                        }
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

        static void SaveToTypeMap(Type type, string id)
        {
            if (File.Exists(TypeMapFile))
                File.AppendAllText(TypeMapFile, type.FullName + ";" + id + Environment.NewLine);
        }
    }
}
