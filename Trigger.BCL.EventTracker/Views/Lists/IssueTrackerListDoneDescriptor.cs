using System.Collections.Generic;
using System.Linq;
using XForms.Store;
using Trigger.BCL.EventTracker.Model;
using XForms.Commands;

namespace Trigger.BCL.EventTracker
{

    public class IssueTrackerListDoneDescriptor : IssueTrackerListDescriptor
    {
        public IssueTrackerListDoneDescriptor()
        {
            Commands.Clear();

            RegisterCommands<INavigateBackListViewCommand>();
            RegisterCommands<IRefreshListViewCommand>();
        }

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