using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public class User : ModelBase
    {
        string userName;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (Equals(userName, value))
                    return;
                userName = value;

                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }

        string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (Equals(password, value))
                    return;
                password = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Password"));
            }
        }

        string eMail;

        public string EMail
        {
            get
            {
                return eMail;
            }
            set
            {
                if (Equals(eMail, value))
                    return;
                eMail = value;

                OnPropertyChanged(new PropertyChangedEventArgs("EMail"));
            }
        }

        public IList<TimeTracker> TrackedTimes
        {
            get
            {
                return Map.ResolveType<IPersistentStore<TimeTracker>>().LoadAll().Where(p => p.User != null && p.User.UserName == UserName).ToList();
            }
        }

        public IList<IssueTracker> TrackedIssuesCreated
        {
            get
            {
                return Map.ResolveType<IPersistentStore<IssueTracker>>().LoadAll().Where(p => p.CreatedBy != null && p.CreatedBy.UserName == UserName).ToList();
            }
        }

        public IList<IssueTracker> TrackedIssuesResolved
        {
            get
            {
                return Map.ResolveType<IPersistentStore<IssueTracker>>().LoadAll().Where(p => p.ResolvedBy != null && p.ResolvedBy.UserName == UserName).ToList();
            }
        }
    }
}
