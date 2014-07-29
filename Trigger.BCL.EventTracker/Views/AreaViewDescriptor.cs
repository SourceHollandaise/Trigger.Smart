using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class AreaViewDescriptor : ViewDescriptor
    {
        public AreaViewDescriptor()
        {
            GroupItemDescriptions = new List<GroupItemDescription>
            {
                new GroupItemDescription("Area", 1)
                {
                    ViewItemDescriptions = new List<ViewItemDescription>
                    {
                        new ViewItemDescription("Name", 1){ LabelText = "Name", ShowLabel = true },
                        new ViewItemDescription("Description", 2){ LabelText = "Description", ShowLabel = true },
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
            };
        }
    }
}
