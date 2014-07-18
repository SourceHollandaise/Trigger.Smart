using System.IO;
using Trigger.CRM.Persistent;
using System;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.Datastore.Persistent;
using System.Linq;

namespace Trigger.CRM.Services
{
	public class DocumentUpdateService
	{
		public int LoadFromDocumentStore()
		{
			var store = DependencyMapProvider.Instance.ResolveType<IStore>();
			var files = Directory.GetFiles(StoreConfigurator.DocumentStoreLocation, "*.*", SearchOption.AllDirectories);
			var documents = store.LoadAll<Document>().ToList();            

			int counter = 0;
			foreach (var file in files)
			{
				var fi = new FileInfo(file);

				var document = documents.FirstOrDefault(p => p.FileName == fi.Name);

				if (document == null)
				{
					document = new Document
					{
						FileName = fi.Name,
						Subject = fi.Name.Replace(fi.Extension, ""),
					};
					document.Initialize();
					document.Save();
					counter++;
				}
			}

			foreach (var doc in documents)
			{
				var path = Path.Combine(StoreConfigurator.DocumentStoreLocation, doc.FileName);

				if (!File.Exists(path))
					doc.Delete();
			}

			return counter;
		}
	}
}
