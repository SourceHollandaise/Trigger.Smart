using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{

    public class IssueTrackerListDetailViewDescriptor : DetailViewDescriptor<IssueTracker>
    {
        public IssueTrackerListDetailViewDescriptor()
        {
            Commands.Clear();

            RegisterCommands<IDeleteObjectDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IAddFileDetailViewCommand>();

            AutoSave = true;

            IsTaggable = true;

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Issue", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name" },
                                new ViewItemDescription(Fields.GetName(m => m.IssuePriority), 2){ LabelText = "Priority" },
                                new ViewItemDescription(Fields.GetName(m => m.IssueType), 3){ LabelText = "Type" },
                                new ViewItemDescription(Fields.GetName(m => m.Area), 4){ LabelText = "Area" },
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 5){ LabelText = "State" }
                               
                            }
                        },
                        new GroupItemDescription(null, 1)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = false },
                            }
                        }
                    }
                },
                new TabItemDescription("Progress", 3)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Start), 6){ LabelText = "Start" },
                                new ViewItemDescription(Fields.GetName(m => m.StartedBy), 7){ LabelText = "Started by" },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 8){ LabelText = "Resolved" },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 9){ LabelText = "Resolved by" },
                            }
                        }
                    }
                },
                new TabItemDescription("Attachment Preview", 4)
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
