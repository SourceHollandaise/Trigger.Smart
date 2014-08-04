using System.IO;
using Trigger.XForms;
using Trigger.BCL.ParaOffice;
using Trigger.XForms.Commands;
using Trigger.BCL.Common.Model;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Security;

namespace Trigger.BCL.ParaOffice
{

    public class CurrentSBListViewCommand : ICurrentUserListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {
            CurrentSBService.CurrentSB.ShowDetailView();
        }

        public string ID
        {
            get
            {
                return "cmd_current_SB";
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

        protected User CurrentUser
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveInstance<ISecurityInfoProvider>().CurrentUser;
            }
        }
    }
    
}
