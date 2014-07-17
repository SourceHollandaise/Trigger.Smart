using System.Linq;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;

namespace Trigger.CRM.Security
{
    public sealed class DataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName) && !string.IsNullOrWhiteSpace(logonParameters.Password))
            {
                var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>()
                    .FirstOrDefault(p => p.UserName == logonParameters.UserName && p.Password == logonParameters.Password);

                if (user != null)
                {
                    var provider = new SecurityInfoProvider();
                    provider.SetUser(user);

                    DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                    return true;
                }
            }

            return false;
        }
    }
}
