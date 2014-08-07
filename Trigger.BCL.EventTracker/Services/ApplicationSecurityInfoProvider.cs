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
                return DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<ApplicationUser>().FirstOrDefault(p => p.UserName == UserName);
            }
        }
    }
}
