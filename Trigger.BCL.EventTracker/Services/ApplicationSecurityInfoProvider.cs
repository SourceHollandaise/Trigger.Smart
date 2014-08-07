using System.Linq;
using Trigger.BCL.EventTracker.Model;
using XForms.Security;
using XForms.Model;
using XForms.Store;
using XForms.Dependency;

namespace Trigger.BCL.EventTracker.Services
{
    public class ApplicationSecurityInfoProvider : ISecurityInfoProvider
    {
        public string UserName
        {
            get;
            set;
        }

        public User CurrentUser
        {
            get
            {
                return MapProvider.Instance.ResolveType<IStore>().LoadAll<ApplicationUser>().FirstOrDefault(p => p.UserName == UserName);
            }
        }
    }
}
