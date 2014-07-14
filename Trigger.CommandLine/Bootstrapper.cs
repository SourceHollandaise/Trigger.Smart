
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;
using Trigger.Dependency;

namespace Trigger.CommandLine
{
    abstract class Bootstrapper
    {
        protected static IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        protected static void StartUpApplication()
        {
            PersistentStoreInitialzer.InitStore();

            Register();

            CreateInitialObjects();
        }

        protected static void Register()
        {
            Map.RegisterType<IAuthenticate, SystemAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();

            Map.RegisterType<IPersistentStore<User>, XmlPersistentStore<User>>();
            Map.RegisterType<IPersistentStore<Project>, XmlPersistentStore<Project>>();
            Map.RegisterType<IPersistentStore<TimeTracker>, XmlPersistentStore<TimeTracker>>();
            Map.RegisterType<IPersistentStore<IssueTracker>, XmlPersistentStore<IssueTracker>>();
        }

        protected static void CreateInitialObjects()
        {
            var userStore = Map.ResolveType<IPersistentStore<User>>();

            var user = userStore.LoadAll().FirstOrDefault(p => p.UserName == "Administrator" && p.Password == "admin");
            if (user == null)
            {
                user = new User{ UserName = "Administrator", Password = "admin" };
                userStore.Save(user);
            }
        }
    }
}
