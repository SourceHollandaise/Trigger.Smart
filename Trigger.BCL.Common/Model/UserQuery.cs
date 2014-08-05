using Trigger.BCL.Common.Datastore;
using Trigger.XStorable.DataStore;
using Trigger.XForms;
using Eto.Drawing;
using System.IO;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.Common.Model
{
    public static class UserQuery
    {
        public static bool CurrentUserIsAdministrator
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser.AllowAdministration;
            }
        }
    }
    
}
