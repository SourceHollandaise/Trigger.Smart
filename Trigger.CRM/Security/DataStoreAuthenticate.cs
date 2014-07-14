using System.Linq;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using Trigger.CRM.Model;

namespace Trigger.CRM.Security
{
    public sealed class DataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParamters)
        {
            if (!string.IsNullOrWhiteSpace(logonParamters.UserName) && !string.IsNullOrWhiteSpace(logonParamters.Password))
            {
                var user = DependencyMapProvider.Instance.ResolveType<IPersistentStore<User>>().LoadAll()
                    .FirstOrDefault(p => p.UserName == logonParamters.UserName && p.Password == logonParamters.Password);

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
