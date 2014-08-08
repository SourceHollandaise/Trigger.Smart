using System.Collections.Generic;
using XForms.Model;
using XForms.Design;
using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{
    public class UserViewDescriptor : DetailViewDescriptor<User>
    {
        public UserViewDescriptor()
        {
            RegisterCommands<IAddFileDetailViewCommand>();
            IsTaggable = false;
            AutoSave = true;

            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Details", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Benutzer", Required = true, ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                        new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Passwort", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail", Fill = true },
                        new ViewItemDescription(Fields.GetName(m => m.UserSex), 4){ LabelText = "Geschlecht", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.Role), 5){ LabelText = "Rolle", Required = true },
                        new ViewItemDescription(Fields.GetName(m => m.AllowAdministration), 6){ LabelText = "Administration", Visible = ApplicationQuery.CurrentUserIsAdministrator },
                    }
                },
                new GroupItemDescription("Avatar", 2)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Avatar", ShowLabel = false, Fill = true },
                    }
                },
            };
        }
    }
}
