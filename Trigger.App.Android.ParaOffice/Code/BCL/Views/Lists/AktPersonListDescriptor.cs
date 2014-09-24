using System.Collections.Generic;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{

    public class AktPersonListDescriptor : ListViewDescriptor<AktPerson>
    {
        public AktPersonListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.AktAlias);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 1){ ColumnHeaderText = "Akt" },
                new ColumnDescription(Fields.GetName(m => m.PersonAlias), 2){ ColumnHeaderText = "Person" },
                new ColumnDescription(Fields.GetName(m => m.VertreterAlias), 3){ ColumnHeaderText = "Vertreter" },
            };
        }
    }
}
