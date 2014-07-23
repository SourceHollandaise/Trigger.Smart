using System.Linq;
using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Security
{
	public sealed class DataStoreAuthenticate : IAuthenticate
	{
		public bool LogOn(LogonParameters logonParameters)
		{
			if (!string.IsNullOrWhiteSpace(logonParameters.UserName) && !string.IsNullOrWhiteSpace(logonParameters.Password))
			{
				var securePassword = logonParameters.Password;//SecureText.Secure(logonParameters.Password);

				var user = DependencyMapProvider.Instance.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == logonParameters.UserName && p.Password == securePassword);

				if (user != null)
				{
					DependencyMapProvider.Instance.UnregisterInstance<ISecurityInfoProvider>();
					var provider = new SecurityInfoProvider();
					DependencyMapProvider.Instance.RegisterInstance<ISecurityInfoProvider>(provider);
					provider.SetUser(user);

					return true;
				}
			}

			return false;
		}
	}
}
