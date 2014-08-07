using System;
using System.IO;
using System.Linq;
using Trigger.XForms;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker.Services
{

    public class DocumentFileDataService : IFileDataService
    {
        IStoreConfiguration StoreConfig
        {
            get
            {
                return  DependencyMapProvider.Instance.ResolveInstance<IStoreConfiguration>();
            }
        }

        public void OpenFile(IFileData fileData)
        {
            if (!string.IsNullOrWhiteSpace(fileData.FileName))
            {
                var path = Path.Combine(StoreConfig.DocumentStoreLocation, fileData.FileName);

                if (!File.Exists(path))
                    return;
                System.Diagnostics.Process.Start(path);
            }
        }

        public int LoadFromStore()
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var files = Directory.GetFiles(StoreConfig.DocumentStoreLocation, "*.*", SearchOption.AllDirectories);
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
                var path = Path.Combine(StoreConfig.DocumentStoreLocation, doc.FileName);

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

                var fileName = Guid.NewGuid() + file.Extension;

                var targetPath = Path.Combine(StoreConfig.DocumentStoreLocation, fileName);

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
                    fileData.Subject = fileData.FileName;
                }
                catch
                {

                }
            }
        }
    }
}
