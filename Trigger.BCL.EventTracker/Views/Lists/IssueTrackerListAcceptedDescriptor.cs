using System.Collections.Generic;
using System.Linq;
using XForms.Store;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class IssueTrackerListCurrentDescriptor : IssueTrackerListDescriptor
    {
        public override IEnumerable<IStorable> Repository
        {
            get
            {
                var store = XForms.Dependency.MapProvider.Instance.ResolveType<IStore>();

                return store.LoadAll<IssueTracker>().Where(p => p.IssueState == IssueState.Accepted || p.IssueState == IssueState.InProgress)
                    .OrderBy(p => p.IssuePriority).ThenBy(p => p.IssueState);
            }
        }
    }

}