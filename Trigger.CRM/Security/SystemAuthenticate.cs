using Trigger.Dependency;
using System.Security.Principal;

namespace Trigger.CRM.Model
{
    public sealed class SystemAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonparamters)
        {
            var systemUser = WindowsIdentity.GetCurrent().Name;

            if (logonparamters.UserName.Equals(systemUser))
            {
                var provider = new SecurityInfoProvider();
                provider.SetUser(new User{ UserName = systemUser });

                DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                return true;
            }
            return false;
        }
    }
}
