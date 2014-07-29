using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class AreaViewDescriptor : ViewDescriptor
    {
        public AreaViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Area", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Name", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Name", 1){ LabelText = "Name" },
                                new ViewItemDescription("Description", 2){ LabelText = "Description" },
                            }
                        },
                        new GroupItemDescription("Links", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("LinkedDocuments", 1){ LabelText = "Documents", ShowLabel = false },
                                new ViewItemDescription("LinkedIssues", 1){ LabelText = "Issues", ShowLabel = false }
                            }
                        }
                    }
                }
            };
        }
    }
}
