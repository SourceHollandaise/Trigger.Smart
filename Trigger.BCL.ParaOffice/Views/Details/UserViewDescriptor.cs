using System;
using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;
using Trigger.XForms.Commands;

namespace Trigger.BCL.ParaOffice
{
    public class UserViewDescriptor : DetailViewDescriptor<User>
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
                                new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Benutzer", Required = true, ReadOnly = !UserQuery.CurrentUserIsAdministrator },
                                new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Passwort", Required = true },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                                new ViewItemDescription(Fields.GetName(m => m.UserSex), 4){ LabelText = "Geschlecht", Required = true },
                                new ViewItemDescription(Fields.GetName(m => m.Role), 5){ LabelText = "Rolle", Required = true },
                                new ViewItemDescription(Fields.GetName(m => m.AllowAdministration), 6){ LabelText = "Administration", Visible = UserQuery.CurrentUserIsAdministrator },
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
                }
            };
        }
    }
}
