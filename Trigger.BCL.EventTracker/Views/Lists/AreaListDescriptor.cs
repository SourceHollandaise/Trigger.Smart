using System.Collections.Generic;
using Trigger.BCL.EventTracker.Model;
using XForms.Design;

namespace Trigger.BCL.EventTracker
{
    public class AreaListDescriptor : ListViewDescriptor<Area>
    {
        public AreaListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Name);

//            ListDetailView = true;
//            ListDetailViewWithToolbar = true;
//            ListDetailViewColumns = 2;
//            ListDetailViewOrientation = ViewItemOrientation.Vertical;
//
//            DetailView = new AreaListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Name), 1){ ColumnHeaderText = "Area" },
                new ColumnDescription(Fields.GetName(m => m.Description), 2){ ColumnHeaderText = "Area Description" },
            };
        }
    }
}
