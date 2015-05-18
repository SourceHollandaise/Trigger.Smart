using System.Collections.Generic;
using XForms.Commands;
using XForms.Design;
using XForms.Model;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{


    public class ApplicationUserViewDescriptor : DetailViewDescriptor<ApplicationUser>
    {
        public ApplicationUserViewDescriptor()
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
                                new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Username", ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator, LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Password", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.UserSex), 4){ LabelText = "Sex", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Role), 5){ LabelText = "Role", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.AllowAdministration), 6){ LabelText = "Administration", Visible = ApplicationQuery.CurrentUserIsAdministrator, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator, LabelOrientation = LabelOrientation.Top },
                            }
                        },
                        new GroupItemDescription("Avatar", 2)
                        {
                            ViewItemOrientation = ViewItemOrientation.Horizontal,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Avatar", ShowLabel = false, Fill = true },
                                //new ViewItemDescription(EmptySpaceFieldName, 2){ ShowLabel = false, Fill = true },
                            }
                        },

                    }
                },
                new TabItemDescription("Current Issues", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {  
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedIssuesInProgress), 1){ LabelText = "Current Issues", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        }
                    }
                },
                new TabItemDescription("Resolved Issues", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.LinkedIssuesResolved), 1){ LabelText = "Resolved Issues", ShowLabel = false, Fill = true, ListMode = ListPropertyMode.List }
                            }
                        },
                    }
                },
                new TabItemDescription("Areas", 10)
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
