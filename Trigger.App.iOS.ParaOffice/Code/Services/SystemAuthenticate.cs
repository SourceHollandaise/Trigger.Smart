using System.Security.Principal;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.Common.Services
{
    public sealed class SystemAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            var id = WindowsIdentity.GetCurrent();

            if (id != null && id.IsAuthenticated)
            {
                if (logonParameters.UserName.ToLowerInvariant() == id.Name.ToLowerInvariant())
                {
                    var provider = new SecurityInfoProvider();
                    var user = DependencyMapProvider.Instance.ResolveType<User>();
                    user.UserName = id.Name;
                    provider.SetUser(user);

                    DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                    return true;
                }
            }
                
            return false;
        }
    }
}
