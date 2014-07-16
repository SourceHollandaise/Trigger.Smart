using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Trigger.CRM.Model;
using Trigger.CRM.Security;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{
    public static class XmlStoreUtils
    {
        public static int UpdateTypeMapForDocuments()
        {
            var files = Directory.GetFiles(StoreConfigurator.DocumentStoreLocation);
            var store = DependencyMapProvider.Instance.ResolveType<IStore<Document>>();

            foreach (var doc in store.LoadAll())
            {
                if (!string.IsNullOrWhiteSpace(doc.FileName))
                {
                    if (!File.Exists(Path.Combine(StoreConfigurator.DocumentStoreLocation, doc.FileName)))
                        store.Delete(doc.MappingId);
                }
                else
                    store.Delete(doc.MappingId);
            }
   
            int counter = 0;
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
               
                var document = store.LoadAll().FirstOrDefault(p => p.FileName == fi.Name);

                if (document == null)
                {
                    document = new Document
                    {
                        Created = DateTime.Now,
                        FileName = fi.Name,
                        Subject = fi.Name.Replace(fi.Extension, ""),
                        User = DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser
                    };

                    store.Save(document);
                    counter++;
                }
            }

            return counter;
        }

        public static void RestoreTypeMap()
        {
            var files = Directory.GetFiles(StoreConfigurator.DataStoreLocation);

            var newLines = new List<string>();
            var lines = File.ReadAllLines(StoreConfigurator.StoreMap);

            foreach (var file in files)
            {
                var fi = new FileInfo(file);

                if (string.IsNullOrEmpty(fi.Name) || string.IsNullOrEmpty(fi.Extension))
                    continue;

                var line = lines.FirstOrDefault(p => p.Contains(fi.Name));
                if (line != null)
                    newLines.Add(line);
            }
           
            var tempPath = StoreConfigurator.DataStoreLocation + "/typeMap_Temp.json";
            var backupPath = StoreConfigurator.DataStoreLocation + "/typeMap_Backup.json";
           
            File.WriteAllLines(tempPath, newLines);

            System.Threading.Thread.Sleep(1500);

            File.Replace(tempPath, StoreConfigurator.StoreMap, backupPath);
        }
    }
}
