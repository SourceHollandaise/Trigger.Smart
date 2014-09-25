using System;
using System.IO;
using System.Linq;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.ParaOffice
{
    public class DokumentFileDataService : IFileDataService
    {
        IStoreConfiguration StoreConfig
        {
            get
            {
                return  MapProvider.Instance.ResolveInstance<IStoreConfiguration>();
            }
        }

        public void OpenFile(IFileData fileData)
        {
            var path = Path.Combine(StoreConfig.DocumentStoreLocation, fileData.FileName);

            if (!File.Exists(path))
                return;

            System.Diagnostics.Process.Start(path);
        }

        public int LoadFromStore()
        {
            var store = MapProvider.Instance.ResolveType<IStore>();
         
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
                        Art = DokumentArt.Eingang,
                        Medium = DokumentMedium.Sonstiges,
                        Status = DokumentStatus.Geschrieben
                    };
                    dokument.Initialize();
                    dokument.Save();
                    counter++;
                }
            }

            foreach (var dokument in dokumentList)
            {
                if (string.IsNullOrWhiteSpace(dokument.FileName))
                    continue;

                var path = Path.Combine(StoreConfig.DocumentStoreLocation, dokument.FileName);

                if (!File.Exists(path))
                    dokument.Delete();
            }

            return counter;
        }

        public void StoreFile(string sourcePath, bool copy = true)
        {
            throw new NotImplementedException();
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

