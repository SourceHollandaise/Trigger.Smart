using System;
using Trigger.XForms;
using Trigger.XForms.Commands;
using Trigger.BCL.EventTracker.Model;
using Trigger.BCL.EventTracker.Services;

namespace Trigger.BCL.EventTracker
{

    public class TimeTrackerCommand : ITimeTrackerCommand
    {
        TimeTracker timeTracker;

        public void Execute(DetailViewArguments args)
        {

            timeTracker = args.CurrentObject as TimeTracker;
            if (timeTracker != null)
            {
                var service = new TimeTrackerService(timeTracker);
                if (timeTracker.Begin.HasValue && !timeTracker.End.HasValue)
                {
                    service.StopTracking(DateTime.Now);
                    timeTracker.Save();
                    Name = "Stop";
                    ImageName = "Accept16";
                    args.Template.ReloadObject();
                }
                if (!timeTracker.Begin.HasValue)
                {
                    service.StartTracking(DateTime.Now);
                    timeTracker.Save();
                    Name = "Start";
                    ImageName = "Add16";
                    args.Template.ReloadObject();
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_track_time";
            }
        }

        public string Name
        {
            get
            {
                if (timeTracker == null)
                    return "Start";

                if (!timeTracker.Begin.HasValue && !timeTracker.End.HasValue)
                    return "Start";

                return timeTracker.Begin.HasValue && !timeTracker.End.HasValue ? "Stop" : "Start";
            }
        }

        public string ImageName
        {
            get
            {
                if (timeTracker == null)
                    return "Accept16";

                if (!timeTracker.Begin.HasValue && !timeTracker.End.HasValue)
                    return "Accept16";

                return timeTracker.Begin.HasValue && !timeTracker.End.HasValue ? "Accept16" : "Add16";
            }
        }
    }
}
