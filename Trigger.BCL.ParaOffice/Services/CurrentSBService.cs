using System;
using Trigger.XStorable.DataStore;
using System.IO;
using Trigger.XStorable.Dependency;
using System.Linq;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.ParaOffice
{
    public static class CurrentSBService
    {
        public static SB CurrentSB
        {
            get
            {
                var user = DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;

                return DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<SB>().FirstOrDefault(p => p.ID.Equals(user.UserName));
            }
        }
    }
}
