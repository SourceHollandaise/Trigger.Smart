using System.Collections.Generic;
using XForms.Design;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class TimeTrackerViewDescriptor : DetailViewDescriptor<TimeTracker>
    {
        public TimeTrackerViewDescriptor()
        {
            RegisterCommands<ITrackTimeDetailViewCommand>();

            TabItemDescriptions = new List<TabItemDescription>
            {
                new TabItemDescription("Tracked Time", 1)
                {
                    GroupItemDescriptions = new List<GroupItemDescription>
                    {
                        new GroupItemDescription("Details", 1)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Subject), 1){ LabelText = "Subject", ShowLabel = true },
                                new ViewItemDescription(Fields.GetName(m => m.Area), 2){ LabelText = "Area", ShowLabel = true },
                                new ViewItemDescription(Fields.GetName(m => m.User), 3){ LabelText = "User", ShowLabel = true }
                            }
                        },
                        new GroupItemDescription("Time", 2)
                        {
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Begin), 1){ LabelText = "Start", ShowLabel = true },
                                new ViewItemDescription(Fields.GetName(m => m.End), 2){ LabelText = "End", ShowLabel = true },
                                new ViewItemDescription(Fields.GetName(m => m.Duration), 3){ LabelText = "Duration", ShowLabel = true },
                            }
                        },
                        new GroupItemDescription("Description", 3)
                        {
                            Fill = true,
                            ViewItemDescriptions = new List<ViewItemDescription>
                            {
                                new ViewItemDescription(Fields.GetName(m => m.Description), 1){ LabelText = "Description", ShowLabel = false, Fill = true },
                            }
                        }
                    }
                }
            };
        }
    }
}
