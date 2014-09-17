using System.Collections.Generic;
using XForms.Design;
using XForms.Store;
using XForms.Dependency;
using System.Linq;

namespace Trigger.BCL.ParaOffice
{
    public class LeistungListDescriptor : ListViewDescriptor<Leistung>
    {
        public LeistungListDescriptor()
        {
            DefaultSorting = ColumnSorting.Descendig;
            DefaultSortProperty = Fields.GetName(m => m.Datum);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Datum), 1){ ColumnHeaderText = "Datum" },
                new ColumnDescription(Fields.GetName(m => m.AktAlias), 2){ ColumnHeaderText = "Akt" },
                new ColumnDescription(Fields.GetName(m => m.SparteAlias), 3){ ColumnHeaderText = "Sparte" },
                new ColumnDescription(Fields.GetName(m => m.RAAlias), 10){ ColumnHeaderText = "RA" },
                new ColumnDescription(Fields.GetName(m => m.RAZeit), 11){ ColumnHeaderText = "RA-Zeit" },
                new ColumnDescription(Fields.GetName(m => m.RAVerdienst), 12){ ColumnHeaderText = "RA-Verdienst" },
                new ColumnDescription(Fields.GetName(m => m.SKAlias), 20){ ColumnHeaderText = "SK" },
                new ColumnDescription(Fields.GetName(m => m.SKZeit), 21){ ColumnHeaderText = "SK-Zeit" },
                new ColumnDescription(Fields.GetName(m => m.SKVerdienst), 22){ ColumnHeaderText = "SK-Verdienst" },
            };
        }
    }
}
