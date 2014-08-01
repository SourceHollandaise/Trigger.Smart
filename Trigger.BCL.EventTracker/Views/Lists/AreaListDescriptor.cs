using Trigger.XForms;
using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;

namespace Trigger.BCL.EventTracker
{
    public class AreaListDescriptor : ListViewDescriptor<Area>
    {
        public AreaListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Name), 1){ ColumnHeaderText = "Area" },
                new ColumnDescription(Fields.GetName(m => m.Description), 2){ ColumnHeaderText = "Area Description" },
            };
        }
    }
}
