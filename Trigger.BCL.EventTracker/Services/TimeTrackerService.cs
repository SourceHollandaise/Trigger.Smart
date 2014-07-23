
using System;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker.Services
{
	public class TimeTrackerService
	{
		readonly TimeTracker timeTracker;

		public TimeTrackerService(TimeTracker timeTracker)
		{
			this.timeTracker = timeTracker;
		}

		public void StartTracking(DateTime begin)
		{
			if (timeTracker.End.HasValue)
				throw new ArgumentException("Cannot start new tracking before stop!");

			if (timeTracker.User == null)
				throw new ArgumentException("Cannot start tracking if user is null!");

			if (!timeTracker.End.HasValue)
				timeTracker.Begin = begin;
		}

		public void StopTracking(DateTime end, string subject = null, string description = null)
		{
			if (timeTracker.User == null)
				throw new ArgumentException("Cannot track if user is null!");

			if (!timeTracker.Begin.HasValue)
				throw new ArgumentException("Cannot stop tracking before start!");

			if (timeTracker.Begin.HasValue)
			{
				if (end < timeTracker.Begin.Value)
					throw new ArgumentException("End must be greater than Start!");

				timeTracker.End = end;
				timeTracker.Subject = subject;
				timeTracker.Description = description;
			}
		}
	}
}
