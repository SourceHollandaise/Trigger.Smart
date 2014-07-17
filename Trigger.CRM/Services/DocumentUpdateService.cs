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
            var items = store.LoadAll(typeof(Document)).OfType<Document>().ToList();            

            int counter = 0;
            foreach (var file in files)
            {
                var fi = new FileInfo(file);

                var document = items.FirstOrDefault(p => p.FileName == fi.Name);

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

            return counter;
        }
    }
}
