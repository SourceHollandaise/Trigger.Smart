using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{

    public class AreaUserListDescriptor : ListViewDescriptor<AreaUser>
    {
        public AreaUserListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.AreaAlias);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.AreaAlias), 1){ ColumnHeaderText = "Area" },
                new ColumnDescription(Fields.GetName(m => m.UserAlias), 2){ ColumnHeaderText = "User" },
            };
        }
    }
}
