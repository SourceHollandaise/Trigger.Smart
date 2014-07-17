
using Trigger.Dependency;
using Trigger.Datastore.Security;
using Trigger.Datastore.Persistent;

namespace Trigger.CommandLine.Commands
{
    public abstract class ConsoleBaseCommand
    {
        protected static IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        protected static User CurrentUser
        {
            get
            {
                return Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }

        protected static IStore Store
        {
            get
            {
                return Map.ResolveType<IStore>();
            }
        }
    }
}