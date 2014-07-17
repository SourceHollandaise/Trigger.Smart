using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{
    public static class StoreUtils
    {
        public static int UpdateTypeMapForDocuments()
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
