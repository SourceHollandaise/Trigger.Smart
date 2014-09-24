using System.Linq;
using XForms.Dependency;
using XForms.Model;
using XForms.Store;

namespace XForms.Security
{
    public sealed class DataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName))
            {
                var password = logonParameters.Password;

                var user = MapProvider.Instance.ResolveType<IStore>().LoadAll<User>()
                    .FirstOrDefault(p => p.UserName.ToLowerInvariant() == logonParameters.UserName.ToLowerInvariant() && p.Password == password);

                if (user != null)
                {
                    MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().UserName = user.UserName;


                    return true;
                }
            }

            return false;
        }
    }
}
