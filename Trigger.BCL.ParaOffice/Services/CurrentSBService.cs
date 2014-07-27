using System;
using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Dependency;
using System.Linq;
using Trigger.BCL.Common.Security;
using System.Configuration;

namespace Trigger.BCL.ParaOffice
{
    public static class CurrentSBService
    {
        public static SB CurrentSB
        {
            get
            {
                var user = DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;

                return DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<SB>().FirstOrDefault(p => p.ID.Equals(user.UserName));
            }
        }
    }

    public class StoreConfiguration : IStoreConfiguration
    {
        public string DataStoreLocation { get; protected set; }

        public string DocumentStoreLocation { get; protected set; }

        public  void InitStore()
        {
            SetStoreLocation();

            SetDocumentStoreLocation();
        }

        protected virtual void  SetStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DataStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DataStoreLocation = value;
        }

        protected virtual void SetDocumentStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DocumentStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DocumentStoreLocation = value;
        }
    }
}
