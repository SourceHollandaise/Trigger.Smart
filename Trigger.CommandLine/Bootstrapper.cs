
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.CRM.Security;
using System.Configuration;

namespace Trigger.CommandLine
{
    class Bootstrapper
    {
        IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        public void StartUpApplication()
        {
            StoreConfigurator.InitStore();

            Register();

            CreateInitialObjects();
        }

        protected void Register()
        {
            var logonType = ConfigurationManager.AppSettings["LogonType"];
            if (!string.IsNullOrWhiteSpace(logonType))
            {
                if (logonType == "System")
                    Map.RegisterType<IAuthenticate, SystemAuthenticate>();
                if (logonType == "DataStore")
                    Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            }
            else
                Map.RegisterType<IAuthenticate, SystemAuthenticate>();

            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileStore>();
        }

        protected void CreateInitialObjects()
        {
            var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "Admin");

            if (user == null)
            {
                user = new User{ UserName = "Admin" };
                user.SetPassword("admin");
                user.Save();
            }
        }
    }
}
