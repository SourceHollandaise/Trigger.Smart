using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Trigger.CRM.Model;
using Trigger.CRM.Security;

namespace Trigger.CRM.Persistent
{
    public static class XmlPersistentStoreUtils
    {
        public static void UpdateTypeMapForDocuments()
        {
            var files = Directory.GetFiles(PersistentStoreInitialzer.PersistentDocumentStoreLocation);
            var store = Dependency.DependencyMapProvider.Instance.ResolveType<IPersistentStore<Document>>();
         
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                var document = store.LoadAll().FirstOrDefault(p => p.DocumentPath == file);

                if (document == null)
                {
                    document = new Document
                    {
                        DocumentPath = file,
                        Subject = fi.Name.Replace(fi.Extension, ""),
                        User = Dependency.DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser
                    };

                    store.Save(document);
                }
            }
        }

        public static void RestoreTypeMap()
        {
            var files = Directory.GetFiles(PersistentStoreInitialzer.PersistentStoreLocation);

            var newLines = new List<string>();
            var lines = File.ReadAllLines(PersistentStoreInitialzer.PersistentStoreMap);

            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                var fileNameId = fi.Name.Replace(fi.Extension, "");
                var line = lines.FirstOrDefault(p => p.Contains(fileNameId));
                if (line != null)
                    newLines.Add(line);
            }
           
            var tempPath = PersistentStoreInitialzer.PersistentStoreLocation + "/typeMap_Temp.json";
            var backupPath = PersistentStoreInitialzer.PersistentStoreLocation + "/typeMap_Backup.json";
           
            File.WriteAllLines(tempPath, newLines);

            System.Threading.Thread.Sleep(2000);

            File.Replace(tempPath, PersistentStoreInitialzer.PersistentStoreMap, backupPath);
        }
    }
}
