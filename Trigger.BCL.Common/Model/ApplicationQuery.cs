using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.Common.Model
{
    public static class ApplicationQuery
    {
        public static bool CurrentUserIsAdministrator
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.AllowAdministration;
            }
        }

        public static User CurrentUser
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }
    }
}
