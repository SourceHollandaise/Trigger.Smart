using Trigger.BCL.Common.Model;
using Trigger.XStorable.DataStore;
using System.Linq;
using Trigger.XStorable.Dependency;

namespace Trigger.BCL.Common.Security
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
                return DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == UserName);
            }
        }
    }
}
