using System.IO;
using Trigger.BCL.ParaOffice;
using XForms.Commands;
using XForms.Model;
using XForms.Dependency;
using XForms.Security;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class CurrentSBDetailsCommand : ICurrentUserDetailsCommand
    {
        public void Execute(TemplateBase template)
        {
            ApplicationModelQuery.CurrentSB.ShowDetailView();
        }

        public string ID
        {
            get
            {
                return "cmd_current_SB";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.ImageAndText;
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
                return 80;
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
