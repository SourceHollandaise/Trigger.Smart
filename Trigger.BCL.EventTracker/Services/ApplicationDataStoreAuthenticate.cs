using System;
using System.Linq;
using Trigger.BCL.EventTracker.Model;
using XForms.Security;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.EventTracker.Services
{

    public sealed class ApplicationDataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName))
            {
                var password = logonParameters.Password;

                var user = MapProvider.Instance.ResolveType<IStore>().LoadAll<ApplicationUser>()
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
