using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class AreaViewDescriptor : ViewDescriptor
    {
        public AreaViewDescriptor()
        {
            GroupItems = new List<GroupItem>
            {
                new GroupItem("Area", 1)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Name", 1){ LabelText = "Name", ShowLabel = true },
                        new ViewItem("Description", 2){ LabelText = "Middle name", ShowLabel = true },
                    }
                },
                new GroupItem("Links", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("LinkedDocuments", 1){ LabelText = "Documents", ShowLabel = false },
                        new ViewItem("LinkedIssues", 1){ LabelText = "Issues", ShowLabel = false }
                    }
                }
            };
        }
    }
    
}
