using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Trigger.XStorable.Dependency;
using Newtonsoft.Json;

namespace Trigger.XStorable.DataStore
{
    public class FileStore : IStore
    {
        const string StoredFileExtension = ".json";

        public void Save(Type type, IStorable item)
        {
            string typeDir = CreateTypeDirectory(type);
                    
            item.MappingId = item.MappingId ?? DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item, type, new JsonSerializerSettings());
  
            var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

            File.WriteAllText(path, json);
        }

        public void Save<T>(T item) where T: IStorable
        {
            Save(typeof(T), item);
        }

        public void DeleteById(Type type, object itemId)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, itemId + StoredFileExtension);

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

            var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

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

            var path = Path.Combine(typeDir, itemId + StoredFileExtension);

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);
               
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject(content, type) as IStorable;
                if (result != null)
                {
                    LinkedObjectHelper.UpdatePersistentReferences(result);
                    return result;
                }
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

            foreach (var item in Directory.GetFiles(typeDir, "*" + StoredFileExtension))
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

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject(content, type) as IStorable;

                LinkedObjectHelper.UpdatePersistentReferences(result);
                return result;
            }

            return null;
        }

        static string CreateTypeDirectory(Type type)
        {
            if (!Directory.Exists(StoreConfigurator.DataStoreLocation))
                return string.Empty;

            var typeDir = Path.Combine(StoreConfigurator.DataStoreLocation, type.FullName);
            if (!Directory.Exists(typeDir))
                Directory.CreateDirectory(typeDir);

            return typeDir;
        }
    }
}

