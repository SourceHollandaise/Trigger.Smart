using System.Linq;
using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.Common.Services
{
    public sealed class DataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName))
            {
                var password = logonParameters.Password;

                var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>()
                    .FirstOrDefault(p => p.UserName.ToLowerInvariant() == logonParameters.UserName.ToLowerInvariant() && p.Password == password);

                if (user != null)
                {
                    DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().UserName = user.UserName;


                    return true;
                }
            }

            return false;
        }
    }
}
