
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using Trigger.CRM.Persistent;

namespace Trigger.CRM.Model
{
    public class Project : ModelBase
    {
        string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (Equals(name, value))
                    return;
                name = value;

                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
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

        //        public IList<TimeTracker> TrackedTimes
        //        {
        //            get
        //            {
        //                return Map.ResolveType<IPersistentStore<TimeTracker>>().LoadAll().Where(p => p.Project != null && p.Project.name == Name).ToList();
        //            }
        //        }
        //
        //        public IList<IssueTracker> TrackedIssues
        //        {
        //            get
        //            {
        //                return Map.ResolveType<IPersistentStore<IssueTracker>>().LoadAll().Where(p => p.Project != null && p.Project.name == Name).ToList();
        //            }
        //        }
    }
}
