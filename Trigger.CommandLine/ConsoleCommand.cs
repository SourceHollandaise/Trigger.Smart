
using Trigger.Dependency;
using Trigger.CRM.Security;
using Trigger.CRM.Model;

namespace Trigger.CommandLine
{
    struct Commands
    {
        public static string ADD = "ADD ";
        public static string DEL = "DEL ";
        public static string LST = "LST ";
        public static string EXIT = "EXIT";
    }

    public abstract class ConsoleCommand
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