using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class SBListDescriptor : ListViewDescriptor<SB>
    {
        public SBListDescriptor()
        {
            ListShowTags = false;

            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.ID);

            ListDetailView = true;
            ListDetailViewWithToolbar = true;
            ListDetailViewColumns = 4;
            ListDetailViewOrientation = ViewItemOrientation.Vertical;

            DetailView = new SBListDetailViewDescriptor();

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.ID), 1){ ColumnHeaderText = "SB-Kürzel" },
                new ColumnDescription(Fields.GetName(m => m.UserAlias), 2){ ColumnHeaderText = "Benutzer" },
            };
        }
    }
}
