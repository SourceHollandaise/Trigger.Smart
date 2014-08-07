using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;
using XForms.Model;

namespace Trigger.BCL.EventTracker
{
    public class UserViewDescriptor : DetailViewDescriptor<ApplicationUser>
    {
        public UserViewDescriptor()
        {
            RegisterCommands<IAddFileDetailViewCommand>();
            IsTaggable = false;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("User", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Username", ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                                new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Password" },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                                new ViewItemDescription(Fields.GetName(m => m.UserSex), 4){ LabelText = "Sex" },
                                new ViewItemDescription(Fields.GetName(m => m.Role), 5){ LabelText = "Role" },
                                new ViewItemDescription(Fields.GetName(m => m.AllowAdministration), 6){ LabelText = "Administration", Visible = ApplicationQuery.CurrentUserIsAdministrator },
                            }
                        },
                        new GroupItemDescription("Avatar", 2)
                        {
                            ViewItemOrientation = ViewItemOrientation.Horizontal,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Avatar", ShowLabel = false, Fill = true },
                                new ViewItemDescription(EmptySpaceFieldName, 2){ ShowLabel = false, Fill = true },
                            }
                        },

                    }
                },
                new TabItemDescription("Areas", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedAreas), 1){ LabelText = "Area users", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
            };
        }
    }
}
