
using System;

namespace Trigger.CRM.Model
{
    public class TimeTracker : ModelBase
    {
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

                OnPropertyChanged();
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

                OnPropertyChanged();
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

                OnPropertyChanged();
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

                OnPropertyChanged();
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

                OnPropertyChanged();
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

                OnPropertyChanged();
            }
        }

        public TimeSpan? Duration
        {
            get
            {
                return End.HasValue && Begin.HasValue ? End - Begin : null;
            }
        }
    }
}

