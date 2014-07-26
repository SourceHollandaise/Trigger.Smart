using System;
using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Dependency;
using System.Linq;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.ParaOffice
{

    public class ParaOfficeDocumentDataService : IFileDataService
    {
        IStoreConfiguration StoreConfig
        {
            get
            {
                return  DependencyMapProvider.Instance.ResolveInstance<IStoreConfiguration>();
            }
        }

        public int LoadFromStore()
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
         
            var files = Directory.GetFiles(StoreConfig.DocumentStoreLocation, "*.*", SearchOption.AllDirectories);
            var dokumentList = store.LoadAll<Dokument>().ToList();            

            int counter = 0;
            foreach (var file in files)
            {
                var fi = new FileInfo(file);

                var dokument = dokumentList.FirstOrDefault(p => p.FileName == fi.Name);

                if (dokument == null)
                {
                    dokument = new Dokument
                    {
                        FileName = fi.Name,
                        Subject = !string.IsNullOrWhiteSpace(fi.Name) ? !string.IsNullOrWhiteSpace(fi.Extension) ? fi.Name.Replace(fi.Extension, "") : fi.Name : "Unknown filename",
                    };
                    dokument.Initialize();
                    dokument.Save();
                    counter++;
                }
            }

            foreach (var dokument in dokumentList)
            {
                var path = Path.Combine(StoreConfig.DocumentStoreLocation, dokument.FileName);

                if (!File.Exists(path))
                    dokument.Delete();
            }

            return counter;
        }

        public void AddFile(IFileData fileData, string sourcePath, bool copy = true)
        {
            if (File.Exists(sourcePath))
            {
                var file = new FileInfo(sourcePath);

                var targetPath = Path.Combine(StoreConfig.DocumentStoreLocation, file.Name);

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

