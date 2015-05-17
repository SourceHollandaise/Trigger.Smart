
using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class AktArtListDescriptor : ListViewDescriptor<AktArt>
    {
        public AktArtListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Art);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Art), 1){ ColumnHeaderText = "Art" },
                new ColumnDescription(Fields.GetName(m => m.Bemerkung), 2){ ColumnHeaderText = "Bemerkung" },
            };
        }
    }
}
