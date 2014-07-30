using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.Common.Model;

namespace Trigger.BCL.EventTracker
{
    public class UserViewDescriptor : ViewDescriptor<User>
    {
        public UserViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("User", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.UserName), 1){ LabelText = "Username" },
                                new ViewItemDescription(Fields.GetName(m => m.Password), 2){ LabelText = "Password" },
                                new ViewItemDescription(Fields.GetName(m => m.Email), 3){ LabelText = "E-Mail" },
                            }
                        },
                    }
                }
            };
        }
    }
    
}
