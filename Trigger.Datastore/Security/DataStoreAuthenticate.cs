using System.Linq;
using Trigger.Dependency;
using Trigger.Datastore.Persistent;
using Trigger.Datastore.Security;

namespace Trigger.Datastore.Security
{
    public sealed class DataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName) && !string.IsNullOrWhiteSpace(logonParameters.Password))
            {
                var securePassword = SecureText.Secure(logonParameters.Password);

                var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == logonParameters.UserName && p.Password == securePassword);

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
