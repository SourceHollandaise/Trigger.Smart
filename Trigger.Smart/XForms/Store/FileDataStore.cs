using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using XForms.Dependency;

namespace XForms.Store
{
    public class FileDataStore : IStore
    {
        const string StoredFileExtension = ".json";

        public void Save(Type type, IStorable item)
        {
            string typeDir = CreateTypeDirectory(type);
                    
            item.MappingId = item.MappingId ?? MapProvider.Instance.ResolveType<IMappingIdGenerator>().GetId();

            var settings = new Newtonsoft.Json.JsonSerializerSettings();
           
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(item, type, settings);
           
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
                return Load(type, path);
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

            foreach (var item in Directory.EnumerateFiles(typeDir, "*" + StoredFileExtension))
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

                try
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject(content, type) as IStorable;
                    LinkedObjectHelper.UpdateStoredReferences(result);
                    result.OnLoaded();
                    return result;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        static string CreateTypeDirectory(Type type)
        {
            if (!Directory.Exists(StoreConfig.DataStoreLocation))
                return string.Empty;

            var typeDir = Path.Combine(StoreConfig.DataStoreLocation, type.FullName);
            if (!Directory.Exists(typeDir))
                Directory.CreateDirectory(typeDir);

            return typeDir;
        }

        static IStoreConfiguration StoreConfig
        {
            get
            {
                return MapProvider.Instance.ResolveInstance<IStoreConfiguration>();
            }
        }
    }
}

