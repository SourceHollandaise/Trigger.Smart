using System;
using System.IO;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Collections.Generic;

namespace Trigger.CRM.Persistent
{
    public class FileStore : IStore
    {
        public void Save(Type type, IStorable item)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, type.FullName);

                if (!Directory.Exists(subDir))
                    Directory.CreateDirectory(subDir);

                if (item.MappingId == null)
                {
                    var id = DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId().ToString().Replace("-", "");
                    item.MappingId = id;
                }
                var json = ServiceStack.Text.JsonSerializer.SerializeToString(item, type);
                var path = Path.Combine(subDir, item.MappingId + ".json");

                File.WriteAllText(path, json);
            }
        }

        public void DeleteById(Type type, object itemId)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, type.FullName);

                if (!Directory.Exists(subDir))
                    return;

                var path = Path.Combine(subDir, itemId + ".json");

                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public void Delete(Type type, IStorable item)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, type.FullName);

                if (!Directory.Exists(subDir))
                    return;

                var path = Path.Combine(subDir, item.MappingId + ".json");

                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        public IStorable Load(Type type, object itemId)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, type.FullName);

                if (!Directory.Exists(subDir))
                    return null;

                var path = Path.Combine(subDir, itemId + ".json");

                if (File.Exists(path))
                {
                    var content = File.ReadAllText(path);

                    return (IStorable)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
                }
            }

            return null;
        }

        public IEnumerable<IStorable> LoadAll(Type type)
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, type.FullName);

                if (!Directory.Exists(subDir))
                    yield break;

                foreach (var item in Directory.EnumerateFiles(subDir, "*.json"))
                    yield return Load(type, item);
            }
        }

        public IEnumerable<T> LoadAll<T>() where T: IStorable
        {
            if (Directory.Exists(StorePath))
            {
                var subDir = Path.Combine(StorePath, typeof(T).FullName);

                if (!Directory.Exists(subDir))
                    yield break;

                foreach (var item in Directory.EnumerateFiles(subDir, "*.json"))
                    yield return (T)Load(typeof(T), item);
            }
        }

        static IStorable Load(Type type, string path)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                return (IStorable)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
            }

            return null;
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

