
using Trigger.Dependency;
using Trigger.CRM.Security;
using Trigger.CRM.Model;

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
    }
}