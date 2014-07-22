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

		public void Save(Type type, IPersistent item)
		{
			string typeDir = CreateTypeDirectory(type);
                    
			item.MappingId = item.MappingId ?? DependencyMapProvider.Instance.ResolveType<IdGenerator>().GetId();

			var json = ServiceStack.Text.JsonSerializer.SerializeToString(item, type);
			var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

			File.WriteAllText(path, json);
		}

		public void Save<T>(T item) where T: IPersistent
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

		public void DeleteById<T>(object itemId) where T: IPersistent
		{
			DeleteById(typeof(T), itemId);
		}

		public void Delete(Type type, IPersistent item)
		{
			string typeDir = CreateTypeDirectory(type);

			var path = Path.Combine(typeDir, item.MappingId + StoredFileExtension);

			if (File.Exists(path))
				File.Delete(path);
		}

		public void Delete<T>(T item) where T: IPersistent
		{
			Delete(typeof(T), item);
		}

		public IPersistent Load(Type type, object itemId)
		{
			string typeDir = CreateTypeDirectory(type);

			var path = Path.Combine(typeDir, itemId + StoredFileExtension);

			if (File.Exists(path))
			{
				var content = File.ReadAllText(path);

				try
				{
					var result = (IPersistent)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
					PersistentReferenceHelper.UpdatePersistentReferences(result);
					return result;
				}
				catch
				{
					return null;
				}
			}

			return null;
		}

		public T Load<T>(object itemId) where T: IPersistent
		{
			return (T)Load(typeof(T), itemId);
		}

		public IEnumerable<IPersistent> LoadAll(Type type)
		{
			string typeDir = CreateTypeDirectory(type);

			foreach (var item in Directory.EnumerateFiles(typeDir, "*" + StoredFileExtension))
				yield return Load(type, item);

		}

		public IEnumerable<T> LoadAll<T>() where T: IPersistent
		{
			return LoadAll(typeof(T)).OfType<T>();
		}

		static IPersistent Load(Type type, string path)
		{
			if (File.Exists(path))
			{
				var content = File.ReadAllText(path);

				var result = (IPersistent)ServiceStack.Text.JsonSerializer.DeserializeFromString(content, type);
				PersistentReferenceHelper.UpdatePersistentReferences(result);
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

