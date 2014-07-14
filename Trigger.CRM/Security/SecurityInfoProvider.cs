using Trigger.CRM.Model;


namespace Trigger.CRM.Security
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
