using System;
using System.IO;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.Datastore.Persistent
{
    public class FileStore : IStore
    {
        const string StoredFileExtension = ".json";

        public void Save(Type type, IPersistentId item)
        {
            string typeDir = CreateTypeDirectory(type);
                    
            item.MappingId = item.MappingId ?? DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

            var json = ServiceStack.Text.JsonSerializer.SerializeToString(item, type);
            var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

            File.WriteAllText(path, json);
        }

        public void Save<T>(T item) where T: IPersistentId
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

        public void DeleteById<T>(object itemId) where T: IPersistentId
        {
            DeleteById(typeof(T), itemId);
        }

        public void Delete(Type type, IPersistentId item)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

            if (File.Exists(path))
                File.Delete(path);
        }

        public void Delete<T>(T item) where T: IPersistentId
        {
            Delete(typeof(T), item);
        }

        public IPersistentId Load(Type type, object itemId)
        {
            string typeDir = CreateTypeDirectory(type);

            var path = Path.Combine(typeDir, itemId + StoredFileExtension);

            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                var result = (IPersistentId)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
                PersistentReferenceHelper.UpdatePersistentReferences(result);
                return result;
            }

            return null;
        }

        public T Load<T>(object itemId) where T: IPersistentId
        {
            return (T)Load(typeof(T), itemId);
        }

        public IEnumerable<IPersistentId> LoadAll(Type type)
        {
            string typeDir = CreateTypeDirectory(type);

            foreach (var item in Directory.EnumerateFiles(typeDir, "*" + StoredFileExtension))
                yield return Load(type, item);

        }

        public IEnumerable<T> LoadAll<T>() where T: IPersistentId
        {
            return LoadAll(typeof(T)).OfType<T>();
        }

        static IPersistentId Load(Type type, string path)
        {
            if (File.Exists(path))
            {
                var content = File.ReadAllText(path);

                var result = (IPersistentId)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
                PersistentReferenceHelper.UpdatePersistentReferences(result);
                return result;
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

