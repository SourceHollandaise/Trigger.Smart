using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerViewDescriptor : ViewDescriptor
    {
        public IssueTrackerViewDescriptor()
        {
            GroupItems = new List<GroupItem>
            {
                new GroupItem("Details", 1)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Subject", 1){ LabelText = "Name", ShowLabel = true },
                        new ViewItem("IssuePriority", 2){ LabelText = "Priority", ShowLabel = true },
                        new ViewItem("IssueType", 3){ LabelText = "Type", ShowLabel = true },
                        new ViewItem("IssueState", 4){ LabelText = "State", ShowLabel = true },
                        new ViewItem("Area", 5){ LabelText = "Area", ShowLabel = true },
                    }
                },
                new GroupItem("Description", 2)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Description", 2){ LabelText = "Description", ShowLabel = false },
                    }
                },
                new GroupItem("Completition", 3)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("Start", 1){ LabelText = "Start", ShowLabel = true },
                        new ViewItem("Resolved", 2){ LabelText = "Resolved", ShowLabel = true },
                        new ViewItem("ResolvedBy", 3){ LabelText = "Resolved by", ShowLabel = true },
                    }
                },
                new GroupItem("Preview", 4)
                {
                    ViewItems = new List<ViewItem>
                    {
                        new ViewItem("FileName", 1){ LabelText = "Preview", ShowLabel = false }
                    }
                }
            };
        }
    }
    
}
