using System;
using System.IO;
using System.Collections.Generic;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{
    public class XmlStore<T> : IStore<T> where T: IStorable
    {
        public void Save(T item)
        {
            if (Directory.Exists(StorePath))
            {
                if (item.MappingId == null)
                    item.MappingId = DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

                var	json = ServiceStack.Text.XmlSerializer.SerializeToString<T>(item);
                var path = Path.Combine(StorePath, item.MappingId + ".xml");

                if (!IsInTypeMap(item.MappingId))
                    SaveToTypeMap(typeof(T), item.MappingId.ToString(), item.MappingId + ".xml");
                    
                File.WriteAllText(path, json);
            }
        }

        public T Load(object itemId)
        {
            if (Directory.Exists(StorePath))
            {
                var path = Path.Combine(StorePath, itemId + ".xml");

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
            if (Directory.Exists(StorePath))
            {
                var path = Path.Combine(StorePath, itemId + ".xml");

                if (File.Exists(path))
                {
                    File.Delete(path);
                    ClearFromTypeMap();
                }
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
                        var fileName = Path.Combine(StorePath, splitted[2]);
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

        static void SaveToTypeMap(Type type, object itemId, string fileName)
        {
            if (File.Exists(TypeMapFile))
                File.AppendAllText(TypeMapFile, type.FullName + ";" + itemId + ";" + fileName + Environment.NewLine);
        }

        static void ClearFromTypeMap()
        {
            if (File.Exists(TypeMapFile))
                XmlStoreUtils.RestoreTypeMap();
        }

        static bool IsInTypeMap(object itemId)
        {
            foreach (var line in File.ReadAllLines(TypeMapFile))
            {
                var splitted = line.Split(';');
              
                var mappedId = splitted[1];
                if (itemId.ToString().Equals(mappedId))
                    return true;
            }

            return false;
        }

        static string StorePath
        {
            get
            {
                return StoreConfigurator.DataStoreLocation;
            }
        }

        static string TypeMapFile
        {
            get
            {
                return StoreConfigurator.StoreMap;
            }
        }
    }
}