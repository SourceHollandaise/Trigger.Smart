using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;
using XForms.Commands;

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
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.IssuePriority), 2){ LabelText = "Priority", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.IssueType), 3){ LabelText = "Type", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Area), 4){ LabelText = "Area", LabelOrientation = LabelOrientation.Top },
                            }
                        },
                        new GroupItemDescription("Progress", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 1){ LabelText = "State", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Start), 2){ LabelText = "Start", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.StartedBy), 2){ LabelText = "Started by", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 3){ LabelText = "Resolved", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 4){ LabelText = "Resolved by", LabelOrientation = LabelOrientation.Top },
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
