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

            RegisterCommands<IAddFileDetailViewCommand>();
            RegisterCommands<IRefreshDetailViewCommand>();
            RegisterCommands<IDeleteObjectDetailViewCommand>();

            AutoSave = true;

            IsTaggable = true;

            MinHeight = 540;

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
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Name", LabelOrientation = LabelOrientation.Top },
                                //new ViewItemDescription(Fields.GetName(m => m.Description), 2){ LabelText = "Description", ShowLabel = false, Fill = true },
                                new ViewItemDescription(Fields.GetName(m => m.IssuePriority), 2){ LabelText = "Priority", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.IssueType), 3){ LabelText = "Type", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Area), 4){ LabelText = "Area", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.IssueState), 5){ LabelText = "State", LabelOrientation = LabelOrientation.Top },
                               
                            }
                        },
                        new GroupItemDescription(null, 2)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = true, LabelOrientation = LabelOrientation.Top },
                            }
                        },
                        /*

                        */
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
                                new ViewItemDescription(Fields.GetName(m => m.Start), 6){ LabelText = "Start", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.StartedBy), 7){ LabelText = "Started by", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.Resolved), 8){ LabelText = "Resolved", LabelOrientation = LabelOrientation.Top },
                                new ViewItemDescription(Fields.GetName(m => m.ResolvedBy), 9){ LabelText = "Resolved by", LabelOrientation = LabelOrientation.Top },
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
