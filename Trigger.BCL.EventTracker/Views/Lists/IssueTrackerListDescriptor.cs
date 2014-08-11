using XForms.Design;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerListDescriptor : ListViewDescriptor<IssueTracker>
    {
        public IssueTrackerListDescriptor()
        {
            DefaultSorting = ColumnSorting.Descendig;
            DefaultSortProperty = Fields.GetName(m => m.Start);

            ListDetailView = true;
            ListDetailViewColumns = 2;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;
            ListDetailViewWithToolbar = true;

            DetailView = new IssueTrackerListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Issue" },
                new ColumnDescription(Fields.GetName(m => m.IssuePriority), 2){ ColumnHeaderText = "Priority" },
                new ColumnDescription(Fields.GetName(m => m.IssueType), 3){ ColumnHeaderText = "Type" },
                new ColumnDescription(Fields.GetName(m => m.IssueState), 4){ ColumnHeaderText = "State" },
                new ColumnDescription(Fields.GetName(m => m.Start), 5){ ColumnHeaderText = "Started" },
                new ColumnDescription(Fields.GetName(m => m.Resolved), 6){ ColumnHeaderText = "Resolved" },
                new ColumnDescription(Fields.GetName(m => m.AreaAlias), 7){ ColumnHeaderText = "Area" },
            };
        }
    }
}