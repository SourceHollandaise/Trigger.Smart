
using System;
using System.ComponentModel;

namespace Trigger.CRM.Model
{
    public class TimeTracker : ModelBase
    {
        public void StartTracking(DateTime begin)
        {
            if (End.HasValue)
                throw new ArgumentException("Cannot start new tracking before stop!");

            if (User == null)
                throw new ArgumentException("Cannot start tracking if user is null!");

            if (!End.HasValue && User != null)
            {
                User = user;
                Begin = begin;
            }
        }

        public void StopTracking(DateTime end, string subject = null, string description = null)
        {
            if (User == null)
                throw new ArgumentException("Cannot track if user is null!");

            if (!Begin.HasValue)
                throw new ArgumentException("Cannot stop tracking before start!");

            if (Begin.HasValue && User != null)
            {
                if (end < Begin.Value)
                    throw new ArgumentException("End must be greater than Start!");
					
                End = end;
                Subject = subject;
                Description = description;
            }
        }

        Project project;

        public Project Project
        {
            get
            {
                return project;
            }
            set
            {
                if (Equals(project, value))
                    return;
                project = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Project"));
            }
        }

        User user;

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (Equals(user, value))
                    return;
                user = value;

                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }

        DateTime? begin;

        public DateTime? Begin
        {
            get
            {
                return begin;
            }
            set
            {
                if (Equals(begin, value))
                    return;
                begin = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Begin"));
            }
        }

        DateTime? end;

        public DateTime? End
        {
            get
            {
                return end;
            }
            set
            {
                if (Equals(end, value))
                    return;
                end = value;

                OnPropertyChanged(new PropertyChangedEventArgs("End"));
            }
        }

        public TimeSpan? Duration
        {
            get
            {
                return End.HasValue && Begin.HasValue ? End - Begin : null;
            }
        }

        string subject;

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (Equals(subject, value))
                    return;
                subject = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Subject"));
            }
        }

        string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (Equals(description, value))
                    return;
                description = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }
    }
}

