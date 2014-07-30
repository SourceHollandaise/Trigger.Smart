using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerViewDescriptor : ViewDescriptor<IssueTracker>
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
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.IssuePriority), 2){ LabelText = "Priority" },
                                new ViewItemDescription(Fields.GetName(m => m.IssueType), 3){ LabelText = "Type" },
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 4){ LabelText = "State" },
                                new ViewItemDescription(Fields.GetName(m => m.Area), 5){ LabelText = "Area" },
                            }
                        },
                        new GroupItemDescription("Completition", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Start), 1){ LabelText = "Start" },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 2){ LabelText = "Resolved" },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 3){ LabelText = "Resolved by" },
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
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = true },
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
                                new ViewItemDescription(Fields.GetName(m => m.FileName), 1){ LabelText = "Preview file", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}
