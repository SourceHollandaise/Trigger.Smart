using XForms.Dependency;
using XForms.Security;

namespace XForms.Model
{
    public static class ApplicationQuery
    {
        public static bool CurrentUserIsAdministrator
        {
            get
            {
                return MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.AllowAdministration;
            }
        }

        public static User CurrentUser
        {
            get
            {
                return MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }
    }
}
