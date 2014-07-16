
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.CRM.Security;
using System.Configuration;
using Trigger.CommandLine.Commands;

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

            Map.RegisterType<IStore<User>, XmlStore<User>>();
            Map.RegisterType<IStore<Project>, XmlStore<Project>>();
            Map.RegisterType<IStore<TimeTracker>, XmlStore<TimeTracker>>();
            Map.RegisterType<IStore<IssueTracker>, XmlStore<IssueTracker>>();
            Map.RegisterType<IStore<Document>, XmlStore<Document>>();
        }

        protected void CreateInitialObjects()
        {
            var userStore = Map.ResolveType<IStore<User>>();

            var user = userStore.LoadAll().FirstOrDefault(p => p.UserName == "Admin" && p.Password == "a");
            if (user == null)
            {
                user = new User{ UserName = "Admin", Password = "a" };
                userStore.Save(user);
            }
        }
    }
}
