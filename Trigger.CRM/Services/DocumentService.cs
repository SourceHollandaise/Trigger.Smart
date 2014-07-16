using System.IO;
using Trigger.CRM.Persistent;
using System;
using Trigger.CRM.Model;

namespace Trigger.CRM.Services
{
    public class DocumentService
    {
        readonly Document document;

        public DocumentService(Document document)
        {
            this.document = document;
        }

        public void AddFile(string sourcePath, bool copy = true)
        {
            if (File.Exists(sourcePath))
            {
                var file = new FileInfo(sourcePath);

                var targetPath = Path.Combine(StoreConfigurator.DocumentStoreLocation, file.Name);

                if (!sourcePath.Equals(targetPath))
                {
                    if (copy)
                        File.Copy(sourcePath, targetPath);
                    else
                        File.Move(sourcePath, targetPath);
                }

                document.FileName = new FileInfo(targetPath).Name;
            }
        }
    }
    
}
