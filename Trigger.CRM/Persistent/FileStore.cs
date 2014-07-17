using System;
using System.IO;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.CRM.Persistent
{
    public class FileStore : IStore
    {
        public void Save(Type type, IStorable item)
        {
            string typeDir = CreateTypeDirectory(type);
                    
            item.MappingId = item.MappingId ?? DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

            var json = ServiceStack.Text.JsonSerializer.SerializeToString(item, type);
            var path = Path.Combine(typeDir, item.MappingId + ".json");

            File.WriteAllText(path, json);
        }

        public void Save<T>(T item) where T: IStorable
        {
            Save(typeof(T), item);
        }

        public void DeleteById(Type type, object itemId)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, itemId + ".json");

            if (File.Exists(path))
                File.Delete(path);
        }

        public void DeleteById<T>(object itemId) where T: IStorable
        {
            DeleteById(typeof(T), itemId);
        }

        public void Delete(Type type, IStorable item)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, item.MappingId + ".json");

            if (File.Exists(path))
                File.Delete(path);
        }

        public void Delete<T>(T item) where T: IStorable
        {
            Delete(typeof(T), item);
        }

        public IStorable Load(Type type, object itemId)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, itemId + ".json");

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                return (IStorable)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
            }

            return null;
        }

        public T Load<T>(object itemId) where T: IStorable
        {
            return (T)Load(typeof(T), itemId);
        }

        public IEnumerable<IStorable> LoadAll(Type type)
        {
            string typeDir = CreateTypeDirectory(type);

            foreach (var item in Directory.EnumerateFiles(typeDir, "*.json"))
                yield return Load(type, item);

        }

        public IEnumerable<T> LoadAll<T>() where T: IStorable
        {
            return LoadAll(typeof(T)).OfType<T>();
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

        static string CreateTypeDirectory(Type type)
        {
            if (!Directory.Exists(StorePath))
                return string.Empty;

            var typeDir = Path.Combine(StorePath, type.FullName);
            if (!Directory.Exists(typeDir))
                Directory.CreateDirectory(typeDir);

            return  typeDir;
        }
    }
}

