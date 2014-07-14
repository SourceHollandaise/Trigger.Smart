using Trigger.Dependency;
using System.Security.Principal;

namespace Trigger.CRM.Model
{
    public sealed class SystemAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonparamters)
        {
            var id = WindowsIdentity.GetCurrent();

            if (id != null && id.IsAuthenticated)
            {
                if (logonparamters.UserName.Equals(id.Name))
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
