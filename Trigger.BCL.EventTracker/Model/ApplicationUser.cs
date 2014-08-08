using System.Collections.Generic;
using System.Linq;
using XForms.Model;
using XForms.Store;

namespace Trigger.BCL.EventTracker.Model
{

    [System.ComponentModel.DefaultProperty("UserName")]
    [System.ComponentModel.DisplayName("User")]
    public class ApplicationUser : User
    {
        [System.ComponentModel.DisplayName("Linked areas")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(Area))]
        public IEnumerable<Area> LinkedAreas
        {
            get
            {
                var areaUsers = Store.LoadAll<AreaUser>().Where(p => p.Area != null && p.User != null && p.User.MappingId.Equals(MappingId));
                foreach (var item in areaUsers)
                {
                    yield return item.Area;
                }
            }
        }

        [System.ComponentModel.DisplayName("Issues in progress")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(IssueTracker))]
        public IEnumerable<IssueTracker> LinkedIssuesInProgress
        {
            get
            {
                return Store.LoadAll<IssueTracker>().Where(p => p.StartedBy != null && p.StartedBy.MappingId.Equals(MappingId) && p.IssueState == IssueState.InProgress);
            }
        }

        [System.ComponentModel.DisplayName("Closed issues")]
        [System.Runtime.Serialization.IgnoreDataMember]
        [LinkedList(typeof(IssueTracker))]
        public IEnumerable<IssueTracker> LinkedIssuesResolved
        {
            get
            {
                return Store.LoadAll<IssueTracker>().Where(p => p.ResolvedBy != null && p.ResolvedBy.MappingId.Equals(MappingId) && p.IssueState == IssueState.Done);
            }
        }
    }
}
