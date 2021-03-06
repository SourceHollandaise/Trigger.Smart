﻿
using System;
using XForms.Model;
using XForms.Security;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{
    [System.ComponentModel.DefaultProperty("Subject")]
    [System.ComponentModel.DisplayName("Tracked Time")]
    [ImageName("date_time")]
    public class TimeTracker : StorableBase
    {
        public override void Initialize()
        {
            User = Map.ResolveInstance<ISecurityInfoProvider>().CurrentUser as ApplicationUser;
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

                UpdateTracker();
            }
        }

        bool isDone;

        [System.ComponentModel.DisplayName("Done")]
        public bool IsDone
        {
            get
            {
                return isDone;
            }
            protected set
            {
                if (Equals(IsDone, value))
                    return;
                isDone = value;

                OnPropertyChanged();
            }
        }

        string duration;

        public string Duration
        {
            get
            {
                return duration;
            }
            protected set
            {
                if (Equals(duration, value))
                    return;
                duration = value;

                OnPropertyChanged();
            }
        }

        [System.ComponentModel.DisplayName("Area")]
        [System.Runtime.Serialization.IgnoreDataMember]
        public string AreaAlias
        {
            get
            {
                return Area != null ? Area.Name : null;
            }
        }

        Area area;

        [LinkedObject]
        public Area Area
        {
            get
            {
                return area;
            }
            set
            {
                if (Equals(area, value))
                    return;
                area = value;

                OnPropertyChanged();
            }
        }

        public string UserAlias
        {
            get
            {
                return User != null ? User.UserName : null;
            }
        }

        ApplicationUser user;

        [LinkedObject]
        public ApplicationUser User
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

        void UpdateTracker()
        {
            if (End.HasValue)
            {
                IsDone = true;
                Duration = (End - Begin).ToString();
            }
            else
            {
                IsDone = false;
                Duration = null;
            }
        }
    }
}

