using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class IssueTrackerListDescriptor : ListViewDescriptor<IssueTracker>
    {
        public IssueTrackerListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Issue" },
                new ColumnDescription(Fields.GetName(m => m.IssuePriority), 2){ ColumnHeaderText = "Priority" },
                new ColumnDescription(Fields.GetName(m => m.IssueType), 3){ ColumnHeaderText = "Type" },
                new ColumnDescription(Fields.GetName(m => m.IssueState), 4){ ColumnHeaderText = "State" },
                new ColumnDescription(Fields.GetName(m => m.AreaAlias), 5){ ColumnHeaderText = "Area" },
            };
        }
    }
}