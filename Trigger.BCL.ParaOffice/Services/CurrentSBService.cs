using System.Linq;
using Trigger.BCL.Common.Security;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;

namespace Trigger.BCL.ParaOffice
{
    public static class CurrentSBService
    {
        public static SB CurrentSB
        {
            get
            {
                var currentUser = DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;

                return DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<SB>()
                    .FirstOrDefault(p => p.User != null && p.User.MappingId.Equals(currentUser.MappingId));
            }
        }
    }
}
