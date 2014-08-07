using System.Collections.Generic;
using XForms.Design;
using XForms.Model;

namespace Trigger.BCL.EventTracker
{

    public class TagListDescriptor : ListViewDescriptor<Tag>
    {
        public TagListDescriptor()
        {
            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Name), 1){ ColumnHeaderText = "Name" },
            };
        }
    }
}
