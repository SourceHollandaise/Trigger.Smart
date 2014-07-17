using Trigger.Datastore.Security;


namespace Trigger.Datastore.Security
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
