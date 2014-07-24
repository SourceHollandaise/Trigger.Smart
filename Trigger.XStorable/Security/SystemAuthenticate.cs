using System.Security.Principal;
using Trigger.XStorable.Dependency;
using Trigger.XStorable.Model;

namespace Trigger.XStorable.Security
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
                    provider.SetUser(new User{ UserName = id.Name });

                    DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                    return true;
                }
            }
                
            return false;
        }
    }
}
