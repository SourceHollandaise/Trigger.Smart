using System.Linq;
using XForms.Model;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Security
{
    public class SecurityInfoProvider : ISecurityInfoProvider
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
                return MapProvider.Instance.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == UserName);
            }
        }
    }
}
