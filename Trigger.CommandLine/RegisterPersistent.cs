
using System.Linq;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;

namespace Trigger.CommandLine
{
    class RegisterPersistent
    {
        static Trigger.Dependency.IDependencyMap Map
        {
            get
            {
                return Trigger.Dependency.DependencyMapProvider.Instance;
            }
        }

        public void InitStore()
        {
            PersistentStoreInitialzer.InitStore("/Users/trigger/PersistentFileDB");
        }

        public void RegisterStores()
        {
            Map.RegisterType<IPersistentStore<User>, XmlPersistentStore<User>>();
            Map.RegisterType<IPersistentStore<Project>, XmlPersistentStore<Project>>();
            Map.RegisterType<IPersistentStore<TimeTracker>, XmlPersistentStore<TimeTracker>>();
            Map.RegisterType<IPersistentStore<IssueTracker>, XmlPersistentStore<IssueTracker>>();
        }

        public void InitTypes()
        {
            Map.RegisterType<IAuthenticate, SystemAuthenticate>();

            var userStore = Map.ResolveType<IPersistentStore<User>>();

            var user = userStore.LoadAll().FirstOrDefault(p => p.UserName == "j.egger" && p.Password == "1234");
            if (user == null)
            {
                user = new User{ UserName = "j.egger", Password = "1234" };
                userStore.Save(user);
            }
        }
    }
}
