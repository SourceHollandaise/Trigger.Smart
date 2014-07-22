using System.IO;
using Trigger.CRM.Persistent;
using System;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.Datastore.Persistent;
using System.Linq;

namespace Trigger.CRM.Services
{
	public class DocumentDataService : IFileDataService
	{
		public int LoadFromStore()
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
						Subject = !string.IsNullOrWhiteSpace(fi.Name) ? !string.IsNullOrWhiteSpace(fi.Extension) ? fi.Name.Replace(fi.Extension, "") : fi.Name : "Unknown filename",
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

		public void AddFile(IFileData fileData, string sourcePath, bool copy = true)
		{
			if (File.Exists(sourcePath))
			{
				var file = new FileInfo(sourcePath);

				var targetPath = Path.Combine(StoreConfigurator.DocumentStoreLocation, file.Name);

				try
				{
					if (!sourcePath.Equals(targetPath))
					{
						if (copy)
							File.Copy(sourcePath, targetPath);
						else
							File.Move(sourcePath, targetPath);
					}

					fileData.FileName = new FileInfo(targetPath).Name;
				}
				catch
				{

				}
			}
		}
	}
}
