using System.Linq;
using Trigger.Dependency;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public sealed class LogonParameters
    {
        public bool Logon(string userName, string password)
        {
            return IsValid(userName, password);
        }

        bool IsValid(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                var user = DependencyMapProvider.Instance.ResolveType<IPersistentStore<User>>().LoadAll().FirstOrDefault(p => p.UserName == userName && p.Password == password);

                if (user != null)
                {
                    var provider = new SecurityInfoProvider();
                    provider.SetUser(user);

                    DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);

                    return true;
                }
            }

            return false;
        }
    }
}
