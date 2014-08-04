using System;
using System.IO;
using System.Linq;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.Common.Security;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker.Services
{

    public sealed class ApplicationDataStoreAuthenticate : IAuthenticate
    {
        public bool LogOn(LogonParameters logonParameters)
        {
            if (!string.IsNullOrWhiteSpace(logonParameters.UserName))
            {
                var password = logonParameters.Password;

                var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<ApplicationUser>()
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
