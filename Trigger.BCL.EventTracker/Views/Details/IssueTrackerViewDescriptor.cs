using System.Collections.Generic;
using Trigger.XForms;
using Trigger.XForms.Commands;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerViewDescriptor : DetailViewDescriptor<IssueTracker>
    {
        public IssueTrackerViewDescriptor()
        {
            RegisterCommands<IAddFileDetailViewCommand>();

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
                                new ViewItemDescription(Fields.GetName(m => m.Area), 4){ LabelText = "Area" },
                            }
                        },
                        new GroupItemDescription("Progress", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 1){ LabelText = "State" },
                                new ViewItemDescription(Fields.GetName(m => m.Start), 2){ LabelText = "Start" },
                                new ViewItemDescription(Fields.GetName(m => m.StartedBy), 2){ LabelText = "Started by" },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 3){ LabelText = "Resolved" },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 4){ LabelText = "Resolved by" },
                            }
                        },
                        new GroupItemDescription("Description", 3)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = true },
                            }
                        },
                    }
                },
                new TabItemDescription("Attachment Preview", 3)
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
