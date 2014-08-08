using System.Linq;
using XForms.Dependency;
using XForms.Security;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{
    public static class CurrentSBService
    {
        public static SB CurrentSB
        {
            get
            {
                var currentUser = MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;

                if (currentUser == null)
                    return null;

                var sb = MapProvider.Instance.ResolveType<IStore>().LoadAll<SB>().FirstOrDefault(p => p.User != null && p.User.UserName == currentUser.UserName);

                return sb;
            }
        }
    }
}
