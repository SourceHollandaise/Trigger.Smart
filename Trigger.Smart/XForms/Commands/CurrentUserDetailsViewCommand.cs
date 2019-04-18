using XForms.Model;
using XForms.Dependency;
using XForms.Security;
using XForms.Design;
using System.IO;


namespace XForms.Commands
{

    public class CurrentUserDetailsViewCommand : ICurrentUserDetailsCommand
    {

        public void Execute(TemplateBase template)
        {
            CurrentUser.ShowDetailContentEmbedded();
        }

        public string ID => "cmd_current_User";

        public ButtonDisplayStyle DisplayStyle => ButtonDisplayStyle.ImageAndText;

        public string Name
        {
            get
            {
                if (CurrentUser != null)
                    return CurrentUser.UserName;
                return "My Profile";
            }
        }

        public string ImageName
        {
            get
            {
                if (CurrentUser == null)
                    return "user_accept";

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

        public int Width => 110;

        public bool AllowExecute => true;

        public bool Visible => true;

        protected User CurrentUser => MapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
    }
}
