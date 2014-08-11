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

            IsTaggable = false;

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
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 5){ LabelText = "State" },
                                new ViewItemDescription(Fields.GetName(m => m.Description), 6){ LabelText = "Description", ShowLabel = false, Fill = true },
                                new ViewItemDescription(EmptySpaceFieldName, 7){ ShowLabel = false }
                            }
                        },
                    }
                },
                new TabItemDescription("Progress", 2)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription(null, 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Start), 2){ LabelText = "Start" },
                                new ViewItemDescription(Fields.GetName(m => m.StartedBy), 2){ LabelText = "Started by" },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 3){ LabelText = "Resolved" },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 4){ LabelText = "Resolved by" },
                            }
                        }
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
