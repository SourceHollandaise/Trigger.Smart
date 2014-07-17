using Trigger.Dependency;
using System.Security.Principal;
using Trigger.Datastore.Security;

namespace Trigger.Datastore.Security
{
    public sealed class SystemAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            var id = WindowsIdentity.GetCurrent();

            if (id != null && id.IsAuthenticated)
            {
                if (logonParameters.UserName.Equals(id.Name))
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