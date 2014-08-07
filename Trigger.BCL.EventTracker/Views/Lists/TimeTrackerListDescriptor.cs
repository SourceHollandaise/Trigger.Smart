using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{

    public class TimeTrackerListDescriptor : ListViewDescriptor<TimeTracker>
    {
        public TimeTrackerListDescriptor()
        {
            DefaultSorting = ColumnSorting.Descendig;
            DefaultSortProperty = Fields.GetName(m => m.Begin);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Subject), 1){ ColumnHeaderText = "Subject" },
                new ColumnDescription(Fields.GetName(m => m.Begin), 2){ ColumnHeaderText = "Start" },
                new ColumnDescription(Fields.GetName(m => m.Begin), 3){ ColumnHeaderText = "End" },
                new ColumnDescription(Fields.GetName(m => m.UserAlias), 4){ ColumnHeaderText = "User" },
            };
        }
    }
}
