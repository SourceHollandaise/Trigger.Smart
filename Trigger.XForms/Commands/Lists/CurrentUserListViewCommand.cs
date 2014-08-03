using Trigger.BCL.Common.Security;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;

namespace Trigger.XForms.Commands
{

    public class CurrentUserListViewCommand : ICurrentUserListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {

        }

        public string ID
        {
            get
            {
                return "cmd_current_User";
            }
        }

        public string Name
        {
            get
            {
                if (CurrentUser != null)
                    return CurrentUser.UserName;
                return "Unknown";
            }
        }

        public string ImageName
        {
            get
            {
                if (CurrentUser == null)
                    return "";

                switch (CurrentUser.UserSex)
                {
                    case Sex.Female:
                        return "she_user_accept";
                    case Sex.Male:
                        return "user_accept";
              
                    default:
                        return "user_accept";
                }
            }
        }

        protected User CurrentUser
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }
    }
}
