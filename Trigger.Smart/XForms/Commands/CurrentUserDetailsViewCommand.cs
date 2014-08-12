using XForms.Model;
using XForms.Dependency;
using XForms.Security;
using XForms.Design;
using System.IO;


namespace XForms.Commands
{

    public class CurrentUserDetailsViewCommand : ICurrentUserDetailsCommand
    {

        public void Execute(MainViewTemplate template)
        {
            CurrentUser.ShowDetailContentEmbedded();
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

                if (string.IsNullOrEmpty(CurrentUser.FileName))
                {
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

                return CurrentUser.FileName.GetValidPath();
            }
        }

        public int Width
        {
            get
            {
                return 110;
            }
        }

        public bool AllowExecute
        {
            get
            {
                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }

        protected User CurrentUser
        {
            get
            {
                return MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }
    }
}
