using System.Security.Principal;
using XForms.Dependency;
using XForms.Model;

namespace XForms.Security
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
                    var user = MapProvider.Instance.ResolveType<User>();
                    user.UserName = id.Name;
                    provider.UserName = user.UserName;

                    MapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                    return true;
                }
            }
                
            return false;
        }
    }
}
