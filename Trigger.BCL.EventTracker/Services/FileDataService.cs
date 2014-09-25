using System;
using System.IO;
using System.Linq;
using Trigger.BCL.EventTracker.Model;
using XForms.Store;
using XForms.Dependency;
using XForms.Commands;

namespace Trigger.BCL.EventTracker.Services
{

    public class FileDataService : IFileDataService
    {
        IStoreConfiguration StoreConfig
        {
            get
            {
                return MapProvider.Instance.ResolveInstance<IStoreConfiguration>();
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
            var store = MapProvider.Instance.ResolveType<IStore>();
            var files = Directory.GetFiles(StoreConfig.DocumentStoreLocation, "*.*", SearchOption.AllDirectories);

            var documents = store.LoadAll<Document>().ToList();            
            var images = store.LoadAll<ImageItem>().ToList();         

            int counter = 0;
            foreach (var file in files)
            {

                var fileInfo = new FileInfo(file);
                var legalName = fileInfo.Name.Replace(fileInfo.Extension, "");
                if (string.IsNullOrWhiteSpace(legalName))
                    continue;

                if (fileInfo.IsSupportedDocument())
                {
                    var document = documents.FirstOrDefault(p => p.FileName == fileInfo.Name);

                    if (document == null)
                    {
                        document = new Document
                        {
                            FileName = fileInfo.Name,
                            Subject = !string.IsNullOrWhiteSpace(fileInfo.Name) ? !string.IsNullOrWhiteSpace(fileInfo.Extension) ? fileInfo.Name.Replace(fileInfo.Extension, "") : fileInfo.Name : "Unknown filename",
                        };
                        document.Initialize();
                        document.Save();
                        counter++;
                    }
                }

                if (fileInfo.IsSupportedImage())
                {
                    var image = images.FirstOrDefault(p => p.FileName == fileInfo.Name);

                    if (image == null)
                    {
                        image = new ImageItem
                        {
                            FileName = fileInfo.Name,
                            Subject = !string.IsNullOrWhiteSpace(fileInfo.Name) ? !string.IsNullOrWhiteSpace(fileInfo.Extension) ? fileInfo.Name.Replace(fileInfo.Extension, "") : fileInfo.Name : "Unknown filename",
                        };
                        image.Initialize();
                        image.Save();
                        counter++;
                    }
                }
            }

            foreach (var doc in documents)
            {
                var path = Path.Combine(StoreConfig.DocumentStoreLocation, doc.FileName);

                if (!File.Exists(path))
                    doc.Delete();
            }

            foreach (var image in images)
            {
                var path = Path.Combine(StoreConfig.DocumentStoreLocation, image.FileName);

                if (!File.Exists(path))
                    image.Delete();
            }

            return counter;
        }

        public void StoreFile(string sourcePath, bool copy = true)
        {
            if (File.Exists(sourcePath))
            {
                var fileInfo = new FileInfo(sourcePath);

                if (fileInfo.IsSupportedDocument() || fileInfo.IsSupportedImage())
                {
                    var fileName = fileInfo.Name;

                    var result = MapProvider.Instance.ResolveType<IStore>().LoadAll<ImageItem>().Where(p => p.FileName.Equals(fileName));
                    if (result.Any())
                        return;

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

                        IFileData data = null;

                        if (fileInfo.IsSupportedImage())
                            data = new ImageItem();

                        if (fileInfo.IsSupportedDocument())
                            data = new Document();

                        if (data != null)
                        {
                            data.FileName = new FileInfo(targetPath).Name;
                            data.Subject = data.FileName;


                            (data as IStorable).Save();
                        }
                    }
                    catch
                    {

                    }
                }
                else
                    ConfirmationMessages.NotSupportedShow("This filetype is currently not supported!");
            }
        }

        public void AddFile(IFileData fileData, string sourcePath, bool copy = true)
        {
            if (File.Exists(sourcePath))
            {
                var fileInfo = new FileInfo(sourcePath);

                if (fileInfo.IsSupportedDocument() || fileInfo.IsSupportedImage())
                {
                    //var fileName = Guid.NewGuid() + fileInfo.Extension;
                    var fileName = fileInfo.Name;

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
                else
                    ConfirmationMessages.NotSupportedShow("This filetype is currently not supported!");
            }
        }
    }
}
