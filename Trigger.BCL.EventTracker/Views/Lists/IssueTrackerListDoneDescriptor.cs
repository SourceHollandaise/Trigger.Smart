using System.Collections.Generic;
using System.Linq;
using XForms.Store;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class IssueTrackerListDoneDescriptor : IssueTrackerListDescriptor
    {
        public override IEnumerable<IStorable> Repository
        {
            get
            {
                var store = XForms.Dependency.MapProvider.Instance.ResolveType<IStore>();

                return store.LoadAll<IssueTracker>().Where(p => p.IssueState == IssueState.Done || p.IssueState == IssueState.Rejected)
                    .OrderByDescending(p => p.Resolved);
            }
        }
    }
}