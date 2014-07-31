using System;
using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.ParaOffice
{

    public class UserViewDescriptor : DetailViewDescriptor<User>
    {
        public UserViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Benutzer", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Benutzername" },
                                new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Passwort" },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                            }
                        },
                    }
                }
            };
        }
    }
}
