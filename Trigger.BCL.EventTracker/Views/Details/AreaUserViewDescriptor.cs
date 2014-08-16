using System.Collections.Generic;
using XForms.Design;
using XForms.Model;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class AreaUserViewDescriptor : DetailViewDescriptor<AreaUser>
    {
        public AreaUserViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Link area with user", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Area), 1){ LabelText = "Area", ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                                new ViewItemDescription(Fields.GetName(m => m.User), 2){ LabelText = "User", ReadOnly = !ApplicationQuery.CurrentUserIsAdministrator },
                            }
                        },
                    }
                }
            };
        }
    }
}
