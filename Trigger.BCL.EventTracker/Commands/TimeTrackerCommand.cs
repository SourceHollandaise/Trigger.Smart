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
                  
                    timeTracker.ReloadObject();
                }
                if (!timeTracker.Begin.HasValue)
                {
                    service.StartTracking(DateTime.Now);
                    timeTracker.Save();

                    timeTracker.ReloadObject();
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
                return "Record";
            }
        }

        public string ImageName
        {
            get
            {
                return "record";
            }
        }
    }
}
