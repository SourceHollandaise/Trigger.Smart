
using System;
using Trigger.BCL.EventTracker.Model;
using System.Configuration;
using Trigger.XStorable.DataStore;
using System.IO;

namespace Trigger.BCL.EventTracker.Services
{
    public class StoreConfiguration : IStoreConfiguration
    {
        public string DataStoreLocation { get; protected set; }

        public string DocumentStoreLocation { get; protected set; }

        public  void InitStore()
        {
            SetStoreLocation();

            SetDocumentStoreLocation();
        }

        protected virtual void  SetStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DataStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DataStoreLocation = value;
        }

        protected virtual void SetDocumentStoreLocation()
        {
            var value = ConfigurationManager.AppSettings["DocumentStoreLocation"];

            if (!Directory.Exists(value))
                Directory.CreateDirectory(value);

            DocumentStoreLocation = value;
        }
    }

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
