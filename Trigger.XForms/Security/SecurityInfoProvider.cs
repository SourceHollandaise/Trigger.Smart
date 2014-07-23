using Trigger.XStore.Security;

namespace Trigger.XStore.Security
{
	public class SecurityInfoProvider : ISecurityInfoProvider
	{
		User currentUser;

		public User CurrentUser
		{ 
			get
			{
				return currentUser;
			}
		}

		internal void SetUser(User user)
		{
			currentUser = user;
		}
	}
}
