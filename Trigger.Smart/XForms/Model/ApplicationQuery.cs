using XForms.Dependency;
using XForms.Security;
using Eto.Drawing;

namespace XForms.Model
{
    public static class ApplicationQuery
    {
        public static bool CurrentUserIsAdministrator
        {
            get
            {
                try
                {
                    var provider = MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>();
                    if (provider == null || provider.CurrentUser == null)
                        return false;
                    return provider.CurrentUser.AllowAdministration;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static User CurrentUser
        {
            get
            {
                try
                {
                    var provider = MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>();
                    if (provider == null || provider.CurrentUser == null)
                        return null;
                
                    return provider.CurrentUser;
                }
                catch
                {
                    return null;
                }
            }
        }
    }

}
