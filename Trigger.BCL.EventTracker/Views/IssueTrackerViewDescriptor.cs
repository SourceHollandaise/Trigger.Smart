using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerViewDescriptor : ViewDescriptor
    {
        public IssueTrackerViewDescriptor()
        {
            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Issue", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Subject", 1){ LabelText = "Name" },
                                new ViewItemDescription("IssuePriority", 2){ LabelText = "Priority" },
                                new ViewItemDescription("IssueType", 3){ LabelText = "Type" },
                                new ViewItemDescription("IssueState", 4){ LabelText = "State" },
                                new ViewItemDescription("Area", 5){ LabelText = "Area" },
                            }
                        },
                        new GroupItemDescription("Completition", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Start", 1){ LabelText = "Start" },
                                new ViewItemDescription("Resolved", 2){ LabelText = "Resolved" },
                                new ViewItemDescription("ResolvedBy", 3){ LabelText = "Resolved by" },
                            }
                        }
                    }
                },
                new TabItemDescription("Description", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Description", 1){ LabelText = "Description", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                },
                new TabItemDescription("Preview", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("FileName", 1){ LabelText = "Preview file", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}
