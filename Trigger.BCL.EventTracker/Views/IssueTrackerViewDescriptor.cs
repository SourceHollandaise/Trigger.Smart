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
                                new ViewItemDescription("Subject", 1){ LabelText = "Name", ShowLabel = true },
                                new ViewItemDescription("IssuePriority", 2){ LabelText = "Priority", ShowLabel = true },
                                new ViewItemDescription("IssueType", 3){ LabelText = "Type", ShowLabel = true },
                                new ViewItemDescription("IssueState", 4){ LabelText = "State", ShowLabel = true },
                                new ViewItemDescription("Area", 5){ LabelText = "Area", ShowLabel = true },
                            }
                        },
                        new GroupItemDescription("Description", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Description", 2){ LabelText = "Description", ShowLabel = false },
                            }
                        },
                        new GroupItemDescription("Completition", 3)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription("Start", 1){ LabelText = "Start", ShowLabel = true },
                                new ViewItemDescription("Resolved", 2){ LabelText = "Resolved", ShowLabel = true },
                                new ViewItemDescription("ResolvedBy", 3){ LabelText = "Resolved by", ShowLabel = true },
                            }
                        }
                    }
                },
                new TabItemDescription("Preview", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Preview", 1)
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
