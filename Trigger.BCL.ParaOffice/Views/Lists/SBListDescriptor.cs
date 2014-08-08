using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class SBListDescriptor : ListViewDescriptor<SB>
    {
        public SBListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.ID);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.ID), 1){ ColumnHeaderText = "SB-Kürzel" },
                new ColumnDescription(Fields.GetName(m => m.UserAlias), 2){ ColumnHeaderText = "Benutzer" },
            };
        }
    }
}
