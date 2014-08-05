using System;
using Trigger.XForms;
using System.Collections.Generic;

namespace Trigger.BCL.ParaOffice
{

    public class AktListDescriptor : ListViewDescriptor<Akt>
    {
        public AktListDescriptor()
        {
            DefaultSorting = ColumnSorting.Ascending;
            DefaultSortProperty = Fields.GetName(m => m.Bezeichnung);

            ColumnDescriptions = new List<ColumnDescription>
            {
                new ColumnDescription(Fields.GetName(m => m.Bezeichnung), 1){ ColumnHeaderText = "Bezeichnung" },
                new ColumnDescription(Fields.GetName(m => m.AktArtAlias), 2){ ColumnHeaderText = "Aktart" },
                new ColumnDescription(Fields.GetName(m => m.AnlageDatum), 3){ ColumnHeaderText = "Anlage" },
                new ColumnDescription(Fields.GetName(m => m.SB1Alias), 4){ ColumnHeaderText = "SB 1" },
                new ColumnDescription(Fields.GetName(m => m.SB2Alias), 4){ ColumnHeaderText = "SB 2" },
            };
        }
    }

}
