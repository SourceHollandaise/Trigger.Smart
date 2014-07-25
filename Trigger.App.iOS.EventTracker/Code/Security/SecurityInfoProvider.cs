using Trigger.BCL.Common.Model;

namespace Trigger.BCL.Common.Security
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

        public void SetUser(User user)
        {
            currentUser = user;
        }
    }
}
