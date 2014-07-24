using Trigger.XStorable.Model;

namespace Trigger.XStorable.Security
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
