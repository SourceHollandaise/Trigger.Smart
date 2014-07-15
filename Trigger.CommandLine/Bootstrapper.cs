
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.Dependency;
using Trigger.CRM.Security;

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
            PersistentStoreInitialzer.InitStore();

            Register();

            CreateInitialObjects();
        }

        protected void Register()
        {
            Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();

            Map.RegisterType<IPersistentStore<User>, XmlPersistentStore<User>>();
            Map.RegisterType<IPersistentStore<Project>, XmlPersistentStore<Project>>();
            Map.RegisterType<IPersistentStore<TimeTracker>, XmlPersistentStore<TimeTracker>>();
            Map.RegisterType<IPersistentStore<IssueTracker>, XmlPersistentStore<IssueTracker>>();
        }

        protected void CreateInitialObjects()
        {
            var userStore = Map.ResolveType<IPersistentStore<User>>();

            var user = userStore.LoadAll().FirstOrDefault(p => p.UserName == "Admin" && p.Password == "a");
            if (user == null)
            {
                user = new User{ UserName = "Admin", Password = "a" };
                userStore.Save(user);
            }
        }
    }
}
