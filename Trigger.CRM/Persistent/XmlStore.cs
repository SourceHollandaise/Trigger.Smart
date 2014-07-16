using System.IO;
using System.Collections.Generic;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{
    public class XmlStore<T> : IStore<T> where T : IStorable
    {
        public void Save(T item)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof(T).FullName);

                if (!Directory.Exists(subDir))
                    Directory.CreateDirectory(subDir);

                if (item.MappingId == null)
                {
                    var id = DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId().ToString().Replace("-", "");
                    item.MappingId = id;
                }
                var json = ServiceStack.Text.JsonSerializer.SerializeToString<T>(item);
                var path = Path.Combine(subDir, item.MappingId + ".json");

                File.WriteAllText(path, json);
            }
        }

        public T Load(object itemId)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof(T).FullName);

                if (!Directory.Exists(subDir))
                    return default(T);

                var path = Path.Combine(subDir, itemId + ".json");

                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    var result = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(content);
                    return result;
                }
            }

            return default(T);
        }

        public void DeleteById(object itemId)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof(T).FullName);

                if (!Directory.Exists(subDir))
                    return;

                var path = Path.Combine(subDir,itemId + ".json");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public void Delete(T item)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof(T).FullName);

                if (!Directory.Exists(subDir))
                    return;

                var path = Path.Combine(subDir, item.MappingId + ".json");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public IEnumerable<T> LoadAll()
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof (T).FullName);

                if (!Directory.Exists(subDir))
                    yield break;

                foreach (var item in Directory.EnumerateFiles(subDir, "*.json"))
                {
                    yield return Load(item);
                }
            }
        }

        static T Load(string path)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                var result = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(content);
                return result;
            }

            return default(T);
        }

        static string StorePath
        {
            get
            {
                return StoreConfigurator.DataStoreLocation;
            }
        }
    }
}